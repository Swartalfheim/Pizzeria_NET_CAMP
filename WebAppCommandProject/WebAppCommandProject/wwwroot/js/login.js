document.getElementById("loginBtn").addEventListener("click", async function (e) {
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;
    await Login(email, password);
});

async function Login(email, password) {
    try {
        const response = await fetch("login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify({
                Email: email,
                Password: password
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
