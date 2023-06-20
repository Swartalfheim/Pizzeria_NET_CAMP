const PizzaDough = ["Thin", "Thick", "WithFilling"];

const Size = ["ExtraLarge", "Large", "Medium", "Small"];

const Category = ["Drinks", "Pizzas", "Sweets"];

const WalletType = ["Cash", "PaymentService", "Card"];



const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

hubConnection.start().catch(function (err) {
    return console.error(err.toString());
});

//hubConnection.on('UpdateMenu', function (menuItemData) {

//    console.log(menuItemData);

//    let currentRow = JSON.parse(menuItemData);
//    addNewRow(currentRow);
//});

async function createMenuOnDialog() {
    let menu = JSON.parse(await GetMenu());
    InitMenu(menu);
};

function InitMenu(menu) {

    var table = document.getElementById("chef");
    var tbody = table.getElementsByTagName("tbody")[0];

    while (tbody.firstChild) {
        tbody.removeChild(tbody.firstChild);
    }



    for (var i = 0; i < menu.length; i++) {
        addNewRow(menu[i]);
    }
};


async function GetMenu() {
    try {
        const response = await fetch("/api/Client/get-menu", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });
        if (response.ok) {
            let menu = await response.json()
            //console.log(menu);
            return menu;
        }
    } catch (e) {
        console.error(e.toString())
    }
};


async function createCashRegOnPage() {
    let cashReg = JSON.parse(await GetCashReg());
    InitCashReg(cashReg);
};

function InitCashReg(cashReg) {
    for (var i = 0; i < cashReg.length; i++) {
        createCashRegister(cashReg[i]);
    }
};


async function GetCashReg() {
    try {
        const response = await fetch("/api/Client/get-cashreg", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        });
        if (response.ok) {
            let menu = await response.json()
            //console.log(menu);
            return menu;
        }
    } catch (e) {
        console.error(e.toString())
    }
};

function createOrderDialog(id) {
    let dialog = document.getElementById('createOrderDialog');

    //input part
    var dialogElement = document.getElementById('cashboxInput');
    var inputElement = document.getElementById(id);

    var value = inputElement.getAttribute('data-value1');

    dialogElement.setAttribute('value', value);


    //select part
    var selectElement = document.getElementById('paymentInput');
    var listItems = document.querySelectorAll('#' + id + ' li');

    selectElement.innerHTML = '';

    listItems.forEach(function (item, i) {
        var option = document.createElement('option');
        option.text = item.textContent;
        option.value = i+1;
        selectElement.appendChild(option);
    });

    dialog.showModal(); // Show the dialog as a modal

    createMenuOnDialog();
}

function closeOrderDialog() {
    let dialog = document.getElementById('createOrderDialog');
    dialog.close(); // Close the dialog
}






function incrementValue(inputId) {
    var inputField = document.getElementById(inputId);
    var value = parseInt(inputField.value);
    inputField.value = value + 1;
    // console.log(value);
}

function decrementValue(inputId) {
    var inputField = document.getElementById(inputId);
    var value = parseInt(inputField.value);
    if (value > 0) {
        inputField.value = value - 1;
    }
    // console.log(value);
}






document.getElementById("CustomerForm").addEventListener("submit", function (event) {
    event.preventDefault(); // Prevent form submission

    let dataForOrder = getFormData();

    closeOrderDialog();

    SendEndpointOrderData(dataForOrder);
    // You can perform further processing or send the data to a server here
});




function getFormData() {
    formData = {}; // Reset form data

    var cashbox = document.getElementById("cashboxInput").value;
    var name = document.getElementById("customerNameInput").value;
    var payment = document.getElementById("paymentInput").value;

    var table = document.getElementById("chef").getElementsByTagName("tbody")[0];
    var rowCount = table.rows.length;

    var inputGroups = document.querySelectorAll('.input-group');

    for (var i = 0; i < rowCount; i++) {
        var inputField = inputGroups[i].querySelector('input');
        var inputValue = inputField.value;

        if (inputValue != 0) {
            formData[i] = inputValue; // Store value in the object
        }
    }

    let resformData = {
        CasReg: cashbox,
        Name: name,
        Amount: Math.floor(Math.random() * (40000- 2000)) + 2000,
        Payment: payment,
        DishDict: formData
    }

    console.log("Form Data:", resformData);

    return resformData;
};


async function SendEndpointOrderData(data) {
    try {
        let responce = await fetch('/api/Client/create-order', {
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


function addNewRow(menuItem) {

    var table = document.getElementById("chef").getElementsByTagName("tbody")[0];
    var newRow = table.insertRow();
    var rowCount = table.rows.length;
    newRow.setAttribute('data-rowid', rowCount);

    // Create cells for the new row
    var amountCell = newRow.insertCell();
    var numberCell = newRow.insertCell();
    var categoryCell = newRow.insertCell();
    var nameCell = newRow.insertCell();
    var descriptionCell = newRow.insertCell();
    var sizeCell = newRow.insertCell();
    var priceCell = newRow.insertCell();

    // Create the amount input group
    var amountInputGroup = document.createElement("div");
    amountInputGroup.className = "input-group";

    var decrementButton = document.createElement("button");
    decrementButton.className = "btn btn-outline-secondary";
    decrementButton.type = "button";
    decrementButton.setAttribute('onclick', `decrementValue('myInput${rowCount}')`);
    decrementButton.onclick = function () {
        decrementValue('myInput' + rowCount);
    };
    var decrementIcon = document.createElement("i");
    decrementIcon.className = "fas fa-minus";
    decrementButton.appendChild(decrementIcon);
    amountInputGroup.appendChild(decrementButton);

    var amountInput = document.createElement("input");
    amountInput.id = "myInput" + rowCount;
    amountInput.type = "text";
    amountInput.className = "form-control text-center";
    amountInput.value = "0";
    amountInput.readOnly = true;
    amountInputGroup.appendChild(amountInput);

    var incrementButton = document.createElement("button");
    incrementButton.className = "btn btn-outline-secondary";
    incrementButton.type = "button";
    incrementButton.setAttribute('onclick', `incrementValue('myInput${rowCount}')`);
    incrementButton.onclick = function () {
        incrementValue('myInput' + rowCount);
    };
    var incrementIcon = document.createElement("i");
    incrementIcon.className = "fas fa-plus";
    incrementButton.appendChild(incrementIcon);
    amountInputGroup.appendChild(incrementButton);

    amountCell.appendChild(amountInputGroup);

    numberCell.textContent = rowCount;
    categoryCell.textContent = Category[menuItem.Dish.Category];
    nameCell.textContent = menuItem.Dish.Name;
    descriptionCell.textContent = menuItem.Dish.Description;
    sizeCell.textContent = Size[menuItem.Dish.Size];
    priceCell.textContent = menuItem.Price;

};

//[{"PaymentMethod":[2,0,1],"Queue":0},{"PaymentMethod":[2,0,1],"Queue":0}]
function createCashRegister(cashReg) {
    let container = document.getElementById('cashRegisterContainer');
    let count = container.childElementCount;
    // Create the main div element with class "card"
    const cardDiv = document.createElement('div');
    cardDiv.className = 'card';
    cardDiv.style.width = '18rem';

    // Create the image element with class "card-img-top"
    const image = document.createElement('img');
    image.className = 'card-img-top';
    image.src = 'https://www.svgrepo.com/show/134162/cashbox.svg';
    image.alt = 'Cashbox';
    cardDiv.appendChild(image);

    // Create the card body div
    const cardBodyDiv = document.createElement('div');
    cardBodyDiv.className = 'card-body';
    cardDiv.appendChild(cardBodyDiv);

    // Create the title element with class "card-title"
    const title = document.createElement('h5');
    title.className = 'card-title';
    title.textContent = `Cashbox ${count}`;
    cardBodyDiv.appendChild(title);

    // Create the text element with class "card-text"
    const text = document.createElement('p');
    text.className = 'card-text';
    text.textContent = 'Amount of people in queue: InLine';
    cardBodyDiv.appendChild(text);

    // Create the list group element with id "list-1" and data attribute "data-value1"
    const listGroup = document.createElement('ul');
    listGroup.className = 'list-group';
    listGroup.id = `list-${count}`;
    listGroup.setAttribute('data-value1', count);
    cardBodyDiv.appendChild(listGroup);

    let paymentId = cashReg.PaymentMethod; 
    for (var i = 0; i < paymentId.length; i++) {
        const listItem = document.createElement('li');
        listItem.className = 'list-group-item';
        listItem.textContent = WalletType[paymentId[i]];
        listGroup.appendChild(listItem);
    }
    // Create list items inside the list group
    //const items = ['Card', 'Cash', 'PaymentService'];
    //items.forEach(itemText => {
    //    const listItem = document.createElement('li');
    //    listItem.className = 'list-group-item';
    //    listItem.textContent = itemText;
    //    listGroup.appendChild(listItem);
    //});

    // Create the button element with onclick attribute
    const button = document.createElement('button');
    button.type = 'button';
    button.className = 'btn btn-primary';
    button.textContent = 'Make Order';
    button.onclick = function () {
        createOrderDialog(`list-${count}`);
    };
    cardBodyDiv.appendChild(button);

    // Append the card div to the document body or any other desired parent element
    container.appendChild(cardDiv);
};



createCashRegOnPage();