// TODO: add endpoint for simulator
document.getElementById("stopBtn").addEventListener("click", function (e) {
    StopWorker();
});

async function StopWorker() {
    try {
        await fetch('/api/Worker/stop', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
    } catch (error) {
        console.error(error.toString())
    }
};

// TODO: add endpoint for simulator
document.getElementById("startBtn").addEventListener("click", function (e) {
    StartWorker();
});

async function StartWorker() {
    try {
        await fetch('/api/Worker/start', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
    } catch (error) {
        console.error(error.toString())
    }
};

// TODO: add endpoint for simulator
document.getElementById("pauseBtn").addEventListener("click", function (e) {
    PauseWorker();
});

async function PauseWorker() {
    try {
        await fetch('/api/Worker/pause', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
    } catch (error) {
        console.error(error.toString())
    }
};

// TODO: add endpoint for simulator
document.getElementById("continueBtn").addEventListener("click", function (e) {
    ContinueWorker();
});

async function ContinueWorker() {
    try {
        await fetch('/api/Worker/continue', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
    } catch (error) {
        console.error(error.toString())
    }
};