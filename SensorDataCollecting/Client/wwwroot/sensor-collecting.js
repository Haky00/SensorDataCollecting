let absoluteOrientationSensor;
let accelerometer;
let gravitySensor;
let gyroscope;
let linearAccelerationSensor;
let magnetometer;
let relativeOrientationSensor;

let data_absoluteOrientationSensor;
let data_accelerometer;
let data_gravitySensor;
let data_gyroscope;
let data_linearAccelerationSensor;
let data_magnetometer;
let data_relativeOrientationSensor;

function startCollecting() {
    data_gyroscope = [];

    let i_absoluteOrientationSensor = 0;
    let i_accelerometer = 0;
    let i_gravitySensor = 0;
    let i_gyroscope = 0;
    let i_linearAccelerationSensor = 0;
    let i_magnetometer = 0;
    let i_relativeOrientationSensor = 0;
    const showDelay = 10;

    let div_gyroscope = document.getElementById("gyro-info");
    div_gyroscope.textContent = "Starting...";

    for (let i = 0; i < 100; i++) {
        let data = {
            X: -0.1,
            Y: 0.2,
            Z: 1.01
        }
        data_gyroscope.push(data);
    }

    gyroscope = new Gyroscope({ frequency: 60 });
    gyroscope.addEventListener("reading", (e) => {
        i_gyroscope++;
        data_gyroscope.push({
            X: gyroscope.x,
            Y: gyroscope.y,
            Z: gyroscope.z
        });
        if (i_gyroscope == showDelay) {
            div_gyroscope.textContent = `x: ${gyroscope.x} | y: ${gyroscope.y} | z: ${gyroscope.z}`;
            i_gyroscope = 0;
        }
    });
    gyroscope.start();
}

function stopCollecting() {
    gyroscope.stop();
}

function getCollectedData() {
    return JSON.stringify({
        Gyroscope: data_gyroscope
    });
}