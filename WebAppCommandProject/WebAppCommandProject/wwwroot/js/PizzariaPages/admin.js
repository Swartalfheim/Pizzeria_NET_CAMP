"use strict";

const WalletType = ["Cash", "PaymentService", "Card"];

const VipLvls = ["None", "Bronze", "Silver", "Gold"];

const Category = ["Drinks", "Pizzas", "Sweets"];

const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

hubConnection.start().catch(function (err) {
    return console.error(err.toString());
});

hubConnection.on('SendClient', function (clientData) {

    //console.log(clientData);
    var client = JSON.parse(clientData);
    var table = document.getElementById("client").getElementsByTagName("tbody")[0];
    var rowCount = table.rows.length;

    var row = table.insertRow(0);
    row.setAttribute("data-rowid", rowCount + 1);

    var cell1 = row.insertCell(0);
    cell1.innerHTML = client.Id;

    var cell2 = row.insertCell(1);
    cell2.innerHTML = client.Name;

    var cell3 = row.insertCell(2);
    cell3.innerHTML = client.CurrentOrderId;

    var cell4 = row.insertCell(3);
    let levelType = client.VipLvls.map(index => VipLvls[index]);
    cell4.innerHTML = levelType.join('; ');

    var cell5 = row.insertCell(4);
    let waletsArr = client.Wallets.map(item => item.WalletsCategory);
    let walletType = waletsArr.map(index => WalletType[index]);
    cell5.innerHTML = walletType.join('; ');

    var cell6 = row.insertCell(5);

    var button = document.createElement('button');
    button.onclick = function () {
        deleteItemBtn(this);
    };

    // Set the class attribute
    button.className = 'btn-close';

    // Set the button type attribute
    button.type = 'button';

    // Set the aria-label attribute
    button.setAttribute('aria-label', 'Close');

    // Append the button to the document body or another element
    cell6.appendChild(button);
});

hubConnection.on('SendWaiter', function (waiterData) {

    //console.log(waiterData);
    var waiter = JSON.parse(waiterData);
    createWaiterRow(waiter);

});

function createWaiterRow(waiter) {
    var table = document.getElementById("waiter").getElementsByTagName("tbody")[0];
    var rowCount = table.rows.length;

    var row = table.insertRow(rowCount);
    row.setAttribute("data-rowid", rowCount + 1);

    var cell1 = row.insertCell(0);
    cell1.innerHTML = waiter.Id;

    var cell2 = row.insertCell(1);
    cell2.innerHTML = waiter.Info;

    var cell3 = row.insertCell(2);
    cell3.innerHTML = "Wait";

    var cell4 = row.insertCell(3);

    var button = document.createElement('button');
    button.onclick = function () {
        deleteItemBtn(this);
    };

    // Set the class attribute
    button.className = 'btn-close';

    // Set the button type attribute
    button.type = 'button';

    // Set the aria-label attribute
    button.setAttribute('aria-label', 'Close');

    // Append the button to the document body or another element
    cell4.appendChild(button);
}

hubConnection.on('UpdateWaiter', function (waiterData, stateData) {

    //console.log(waiterData + ' - ' + stateData);

    let marker = false;
    var waiter = JSON.parse(waiterData);

    var table = document.getElementById('waiter'); // Get the table element
    var rows = table.getElementsByTagName('tr'); // Get all the rows in the table

    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var rowDataId = row.getAttribute('data-rowid'); // Get the 'data-rowid' attribute value

        if (rowDataId == waiter.Id) {
            // Update the row with the given ID
            row.cells[2].textContent = stateData;
            marker = true;
        }
    }

    if (!marker) {
        createWaiterRow(waiter);
    }
});


hubConnection.on('SendChef', function (chefData) {

    //console.log(chefData);
    var chef = JSON.parse(chefData);
    createChefRow(chef);

});

hubConnection.on('UpdateChef', function (chefData, stateData) {

    //console.log(chefData + ' - ' + stateData);

    let marker = false;
    var chef = JSON.parse(chefData);

    var table = document.getElementById('chef'); // Get the table element
    var rows = table.getElementsByTagName('tr'); // Get all the rows in the table

    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var rowDataId = row.getAttribute('data-rowid'); // Get the 'data-rowid' attribute value

        if (rowDataId == chef.Id) {
            // Update the row with the given ID
            row.cells[2].textContent = stateData;
            marker = true;
        }
    }

    if (!marker) {
        createChefRow(chef);
    }

});


function createChefRow(chef) {
    var table = document.getElementById("chef").getElementsByTagName("tbody")[0];
    var rowCount = table.rows.length;

    var row = table.insertRow(rowCount);
    row.setAttribute("data-rowid", rowCount + 1);

    var cell1 = row.insertCell(0);
    cell1.innerHTML = chef.Id;

    var cell2 = row.insertCell(1);
    cell2.innerHTML = chef.Name;

    var cell3 = row.insertCell(2);
    cell3.innerHTML = "Wait";

    var cell4 = row.insertCell(3);

    var button = document.createElement('button');
    button.onclick = function () {
        deleteItemBtn(this);
    };

    // Set the class attribute
    button.className = 'btn-close';

    // Set the button type attribute
    button.type = 'button';

    // Set the aria-label attribute
    button.setAttribute('aria-label', 'Close');

    // Append the button to the document body or another element
    cell4.appendChild(button);
}



function SendDataWaiter() {
    // Get the form element
    let form = document.getElementById("createWaiterForm");

    // Get the waiter name input value
    let waiterNameInput = document.getElementById("waiterNameInput");
    let waiterName = waiterNameInput.value;


    // Create an object to store the collected data
    let formData = {
        waiterName: waiterName
    };

    // Log the collected data to the console
    console.log(formData);

    SendEndpointWaiterData(waiterName)

};

async function SendEndpointWaiterData(data) {
    try {
        let responce = await fetch(`/api/Admin/create-waiter?waiterName=${data}`, {
            method: 'POST',
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });
        if (responce.ok) {
            let data = response => response.json();
            console.log(data);
        }
    } catch (error) {
        console.error(error)
    }
};


function SendDataChef() { 
    // Get the chef name input value
    let chefNameInput = document.getElementById("chefNameInput");
    let chefName = chefNameInput.value;

    // Get all the checkboxes with the name 'category'
    const checkboxes = document.querySelectorAll('input[name="category"]:checked');

    // Create an array to store the selected categories
    const selectedCategories = [];

    // Iterate through the selected checkboxes and add their values to the array
    checkboxes.forEach((checkbox) => {
        selectedCategories.push(checkbox.value);
    });

    // Create an object to store the collected data
    let formData = {
        Name: chefName,
        CategoryId: selectedCategories
    };

    // Log the collected data to the console
    console.log(formData);
    SendEndpointChefData(formData)
};

async function SendEndpointChefData(data) {
    try {
        let responce = await fetch('/api/Admin/create-chef', {
            method: 'POST',
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });
        if (responce.ok) {
            let data = response => response.json();
            console.log(data);
        }
    } catch (error) {
        console.error(error)
    }
};



////////////////////////////////////////////////////

function SendDataClient() {
    // Get the client name input value
    let clientNameInput = document.getElementById("clientNameInput");
    let clientName = clientNameInput.value;

    // Get the client status input value
    let clientStatusInput = document.getElementById("clientStatusInput");
    let clientStatus = clientStatusInput.value;

    // Get the client walet input value
    let clientWaletInput = document.getElementById("clientWaletInput");
    let clientWalet = clientWaletInput.value;

    // Create an object to store the collected data
    let formData = {
        Name: clientName,
        Amount: Math.floor(Math.random() * (40000 - 2000)) + 2000,
        VipLevel: clientStatus,
        PaymentCategory: clientWalet
    };

    // Log the collected data to the console
    console.log(formData);

    SendClientData(formData);
};

async function SendClientData(formData) {
    try {
        let responce = await fetch('/api/Admin/create-client', {
            method: 'POST',
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            },
            body: JSON.stringify(formData)
        });
        if (responce.ok) {
            let data = response => response.json();
            console.log(data);
        }
    } catch (error) {
        console.error(error)
    }
};








async function DeleteData(data, entity) {
    try {
        let responce = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: data
        });
        if (responce.ok) {
            let data = response => response.json();
            console.log(data);
        }
    } catch (error) {
        console.error(error)
    }
};



document.getElementById("createChefBtn").addEventListener('click', function () {
    SendDataChef();
    closeChefDialog();
});

document.getElementById("CloseChefDialogBtn").addEventListener('click', function () {
    closeChefDialog()
});

function openChefDialog() {
    let dialog = document.getElementById('createChefDialog');
    dialog.showModal(); // Show the dialog as a modal
}

function closeChefDialog() {
    let dialog = document.getElementById('createChefDialog');
    dialog.close(); // Close the dialog
}



document.getElementById("createClientBtn").addEventListener('click', function () {
    SendDataClient();
    closeClientDialog()
});

document.getElementById("CloseClientDialogBtn").addEventListener('click', function () {
    closeClientDialog()
});

function openClientDialog() {
    let dialog = document.getElementById('createClientDialog');
    dialog.showModal(); // Show the dialog as a modal
}

function closeClientDialog() {
    let dialog = document.getElementById('createClientDialog');
    dialog.close(); // Close the dialog
}


document.getElementById("createWaiterBtn").addEventListener('click', function () {
    SendDataWaiter();
    closeWaiterDialog()
});

document.getElementById("CloseWaiterDialogBtn").addEventListener('click', function () {
    closeWaiterDialog()
});

function openWaiterDialog() {
    let dialog = document.getElementById('createWaiterDialog');
    dialog.showModal(); // Show the dialog as a modal
}

function closeWaiterDialog() {
    let dialog = document.getElementById('createWaiterDialog');
    dialog.close(); // Close the dialog
}

function deleteItemBtn(item) {
    let row = item.parentNode.parentNode;

    let id = row.cells[0].innerHTML;
    let entity = row.parentNode.parentNode.getAttribute("id");

    // Create an object to store the collected data
    let formData = {
        id: id,
        entity: entity
    };

    // Log the collected data to the console
    console.log(formData);

    // TODO: - create with new endpoint on backend
    //------------------
    // let url = 'some endpoint'; // use entity to choise endpoint
    // DeleteData(formData, url)
    //------------------

    // row.parentNode.removeChild(row);
};

// TODO: realize function to add new entity to every table
// TODO: realize connection to SignalR!


