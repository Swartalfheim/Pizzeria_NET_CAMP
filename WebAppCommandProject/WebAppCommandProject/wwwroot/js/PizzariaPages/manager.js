const PizzaDough = ["Thin", "Thick", "WithFilling"];

const Size = ["ExtraLarge", "Large", "Medium", "Small"];

const Category = ["Drinks", "Pizzas", "Sweets"];


const hubConnection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

hubConnection.start().catch(function (err) {
	return console.error(err.toString());
});

hubConnection.on('UpdateMenu', function (menuItemData) {

	console.log(menuItemData);

	let currentRow = JSON.parse(menuItemData);
	createMenuRow(currentRow);
});


async function createDishDialog() {
	let dialog = document.getElementById('createDishDialog');
	dialog.showModal(); // Show the dialog as a modal
	let ingedients = JSON.parse(await GetIngredient());
	InitIngradients(ingedients);
}

function closeDishDialog() {
	let dialog = document.getElementById('createDishDialog');
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

document.getElementById("DishForm").addEventListener("submit", function(event) {
	event.preventDefault(); // Prevent form submission
  
	
	let dataForDish = getFormData();

	closeDishDialog();

	SendEndpointDishData(dataForDish);
	// You can perform further processing or send the data to a server here
  });

function getFormData() {
	let formData = {}; // Reset form data

	var name = document.getElementById("dishNameInput").value;
	var description = document.getElementById("descriptionInput").value;
	var time = document.getElementById('timeInput').value;
	var size = document.getElementById('sizeInput').value;
	var dough = document.getElementById('doughInput').value;
	var price = document.getElementById('priceInput').value;


	var textWrappers = document.querySelectorAll('.text-wrapper');
	var inputGroups = document.querySelectorAll('.input-group');
	
	for (var i = 0; i < textWrappers.length; i++) {
		var spanText = textWrappers[i].querySelector('span').innerText;
		var inputField = inputGroups[i].querySelector('input');
		var inputValue = inputField.value;
	  
		if (inputValue != 0) {
			formData[i] = inputValue; // Store value in the object
		}
	}

	let resformData = {
		Name: name,
		Description: description,
		TimePrepare: time,
		Size: size,
		Dough: dough,
		Price: price,
		IngrDict: formData
	}
	
	//console.log("Form Data:", resformData);

	return resformData;
}

function InitIngradients(ingredients) {
	var currInputs = document.getElementById('ingridients');

	var child = currInputs.firstChild;

	// Loop through and remove each subsequent sibling
	while (child.nextSibling) {
		currInputs.removeChild(child.nextSibling);
	}

	for (var i = 0; i < ingredients.length; i++) {
		createIngradientField(ingredients[i].Name)
    }
}

function createIngradientField(ingredient) {

	var inputCount = document.querySelectorAll('.text-wrapper').length;

	++inputCount;

	// Create div element with id and class
	var div = document.createElement('div');
	div.className = 'input-group';

	// Create inner div elements with class
	var prependDiv = document.createElement('div');
	prependDiv.className = 'input-group-prepend';

	var appendDiv = document.createElement('div');
	appendDiv.className = 'input-group-append';

	// Create decrement button element with attributes and event listener
	var decrementBtn = document.createElement('button');
	decrementBtn.className = 'btn btn-outline-secondary';
	decrementBtn.type = 'button';
	decrementBtn.setAttribute('onclick', `decrementValue('myInput${inputCount}')`);

	// Create icon element for decrement button
	var decrementIcon = document.createElement('i');
	decrementIcon.className = 'fas fa-minus';

	// Append icon to decrement button
	decrementBtn.appendChild(decrementIcon);

	// Create input element with attributes
	var input = document.createElement('input');
	input.id = `myInput${inputCount}`;
	input.type = 'text';
	input.className = 'form-control text-center';
	input.value = '0';
	input.readOnly = true;

	// Create increment button element with attributes and event listener
	var incrementBtn = document.createElement('button');
	incrementBtn.className = 'btn btn-outline-secondary';
	incrementBtn.type = 'button';
	incrementBtn.setAttribute('onclick', `incrementValue('myInput${inputCount}')`);

	// Create icon element for increment button
	var incrementIcon = document.createElement('i');
	incrementIcon.className = 'fas fa-plus';

	// Append icon to increment button
	incrementBtn.appendChild(incrementIcon);

	// Create text wrapper element
	var textWrapper = document.createElement('div');
	textWrapper.className = 'text-wrapper';

	// Create span element for text
	var span = document.createElement('span');
	span.textContent = ingredient;

	// Append elements to their respective parent elements
	prependDiv.appendChild(decrementBtn);
	appendDiv.appendChild(incrementBtn);
	textWrapper.appendChild(span);

	div.appendChild(prependDiv);
	div.appendChild(input);
	div.appendChild(appendDiv);
	div.appendChild(textWrapper);

	// Get the existing div with id "ingridients"
	var ingredientsDiv = document.getElementById('ingridients');

	// Append the newly created block to the existing div
	ingredientsDiv.appendChild(div);

}

async function GetIngredient() {
	try {
		const response = await fetch("/api/Manager/get-ingredients", {
			method: "GET",
			headers: {
				"Content-Type": "application/json",
				"Accept": "application/json"
			}
		});
		if (response.ok) {
			let ingredient = await response.json()
			console.log(ingredient);
			return ingredient;
		}
	} catch (e) {
		console.error(e.toString())
	}
};

async function GetMenu() {
	try {
		const response = await fetch("/api/Manager/get-menu", {
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

async function SendEndpointDishData(data) {
	try {
		let responce = await fetch('/api/Manager/create-dish', {
			method: 'POST',
			headers: {
				"Accept": "application/json",
				"Content-Type": "application/json"
			},
			body: JSON.stringify(data)
		});
		if (responce.ok) {
			let data = response => response.json();
			//console.log(data);
		}
	} catch (error) {
		console.error(error)
	}
};

async function createMenuOnPage() {
	let menu = JSON.parse(await GetMenu());
	InitMenu(menu);
};

function InitMenu(menu) {
	for (var i = 0; i < menu.length; i++) {
		createMenuRow(menu[i]);
	}
};

//{ "Dish":{"Name":"asdasda",  "Description":"asdads",     "Category":1,"Dough":1,"Size":1},"Price":342.0}
//[{"Dish":{"Name":"Margarita","Description":"First pizza","Category":1,"Dough":1,"Size":1},"Price":550.0},{"Dish":{"Name":"Pepperoni","Description":"Second pizza","Category":1,"Dough":2,"Size":3},"Price":450.0},{"Dish":{"Name":"Juice","Description":"Delicius juice","Category":0,"Taste":1,"Size":1},"Price":300.0}]
function createMenuRow(menuItem) {
	var table = document.getElementById("chef").getElementsByTagName("tbody")[0];
	var rowCount = table.rows.length;
	++rowCount;
	// Get the table element by its ID
	var table = document.getElementById('chef');

	// Create a new row element
	var newRow = document.createElement('tr');
	newRow.setAttribute('data-rowid', rowCount);

	// Create cells for the new row
	var cell1 = document.createElement('th');
	cell1.setAttribute('scope', 'row');
	cell1.textContent = rowCount;

	var cell2 = document.createElement('td');
	cell2.textContent = Category[menuItem.Dish.Category];

	var cell3 = document.createElement('td');
	cell3.textContent = menuItem.Dish.Name;

	var cell4 = document.createElement('td');
	cell4.textContent = menuItem.Dish.Description;

	var cell5 = document.createElement('td');
	cell5.textContent = Size[menuItem.Dish.Size];

	var cell6 = document.createElement('td');
	cell6.textContent = menuItem.Price;

	var cell7 = document.createElement('td');
	var deleteBtn = document.createElement('button');
	deleteBtn.className = 'btn-close';
	deleteBtn.setAttribute('type', 'button');
	deleteBtn.setAttribute('aria-label', 'Close');
	deleteBtn.addEventListener('click', function () {
		deleteItemBtn(this);
	});
	cell7.appendChild(deleteBtn);

	// Append cells to the new row
	newRow.appendChild(cell1);
	newRow.appendChild(cell2);
	newRow.appendChild(cell3);
	newRow.appendChild(cell4);
	newRow.appendChild(cell5);
	newRow.appendChild(cell6);
	newRow.appendChild(cell7);

	// Append the new row to the table body
	var tbody = table.querySelector('tbody');
	tbody.appendChild(newRow);
}

createMenuOnPage();
