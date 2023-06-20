document.getElementById("registrationBtn").addEventListener("click", async function (e) {
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
    let passwordConfirm = document.getElementById("passwordConfirm").value;
    if (password != passwordConfirm) {
        window.alert("Password and Confirm must be the same!");
    }
    else {
        await Registration(email, password);
    }

});

async function Registration(email, password) {
    try {
        const response = await fetch("registration", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify({
                Email: email,
                Password: password,
                ConfirmPassword: password
            })
        });
        if (response.ok) {
            let message = await response.json()
            window.alert(message.message);
            location.href = window.location.origin;
        }
    } catch (e) {
        console.error(e.toString())
    }
};
