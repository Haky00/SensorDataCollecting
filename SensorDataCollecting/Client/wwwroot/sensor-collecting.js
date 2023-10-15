// God forgive me

let accelerometer;
let gravitySensor;
let gyroscope;
let linearAccelerationSensor;
//let magnetometer;
let absoluteOrientationSensor;
let relativeOrientationSensor;

let data_accelerometer;
let data_gravitySensor;
let data_gyroscope;
let data_linearAccelerationSensor;
//let data_magnetometer;
let data_absoluteOrientationSensor;
let data_relativeOrientationSensor;

const info_show_delay = 6;

function startCollecting() {
    data_accelerometer = [];
    data_gravitySensor = [];
    data_gyroscope = [];
    data_linearAccelerationSensor = [];
    //data_magnetometer = [];
    data_absoluteOrientationSensor = [];
    data_relativeOrientationSensor = [];

    let i_accelerometer = 0;
    let i_gravitySensor = 0;
    let i_gyroscope = 0;
    let i_linearAccelerationSensor = 0;
    //let i_magnetometer = 0;
    let i_absoluteOrientationSensor = 0;
    let i_relativeOrientationSensor = 0;

    let ts_accelerometer = 0;
    let ts_gravitySensor = 0;
    let ts_gyroscope = 0;
    let ts_linearAccelerationSensor = 0;
    //let ts_magnetometer = 0;
    let ts_absoluteOrientationSensor = 0;
    let ts_relativeOrientationSensor = 0;

    let info_accelerometer = document.getElementById("accelerometer-info");
    let info_gravitySensor = document.getElementById("gravitySensor-info");
    let info_gyroscope = document.getElementById("gyroscope-info");
    let info_linearAccelerationSensor = document.getElementById("linearAccelerationSensor-info");
    //let info_magnetometer = document.getElementById("magnetometer-info");
    let info_absoluteOrientationSensor = document.getElementById("absoluteOrientationSensor-info");
    let info_relativeOrientationSensor = document.getElementById("relativeOrientationSensor-info");

    info_accelerometer.textContent = "Starting...";
    info_gravitySensor.textContent = "Starting...";
    info_gyroscope.textContent = "Starting...";
    info_linearAccelerationSensor.textContent = "Starting...";
    //info_magnetometer.textContent = "Starting...";
    info_absoluteOrientationSensor.textContent = "Starting...";
    info_relativeOrientationSensor.textContent = "Starting...";

    accelerometer = new Accelerometer({ frequency: 60 });
    accelerometer.addEventListener("reading", (e) => {
        i_accelerometer = sensorReadXYZ(accelerometer, i_accelerometer, data_accelerometer, info_accelerometer, ts_accelerometer);
    });

    gravitySensor = new GravitySensor({ frequency: 60 });
    gravitySensor.addEventListener("reading", (e) => {
        i_gravitySensor = sensorReadXYZ(gravitySensor, i_gravitySensor, data_gravitySensor, info_gravitySensor, ts_gravitySensor);
    });

    gyroscope = new Gyroscope({ frequency: 60 });
    gyroscope.addEventListener("reading", (e) => {
        i_gyroscope = sensorReadXYZ(gyroscope, i_gyroscope, data_gyroscope, info_gyroscope, ts_gyroscope);
    });

    linearAccelerationSensor = new LinearAccelerationSensor({ frequency: 60 });
    linearAccelerationSensor.addEventListener("reading", (e) => {
        i_linearAccelerationSensor = sensorReadXYZ(linearAccelerationSensor, i_linearAccelerationSensor, data_linearAccelerationSensor, info_linearAccelerationSensor, ts_linearAccelerationSensor);
    });

    //magnetometer = new Magnetometer({ frequency: 60 });
    //magnetometer.addEventListener("reading", (e) => {
    //    i_magnetometer = sensorReadXYZ(magnetometer, i_magnetometer, data_magnetometer, info_magnetometer, ts_magnetometer);
    //});

    absoluteOrientationSensor = new AbsoluteOrientationSensor({ frequency: 60, referenceFrame: "device" });
    absoluteOrientationSensor.addEventListener("reading", (e) => {
        i_absoluteOrientationSensor = sensorReadXYZW(absoluteOrientationSensor, i_absoluteOrientationSensor, data_absoluteOrientationSensor, info_absoluteOrientationSensor, ts_absoluteOrientationSensor);
    });

    relativeOrientationSensor = new RelativeOrientationSensor({ frequency: 60, referenceFrame: "device" });
    relativeOrientationSensor.addEventListener("reading", (e) => {
        i_relativeOrientationSensor = sensorReadXYZW(relativeOrientationSensor, i_relativeOrientationSensor, data_relativeOrientationSensor, info_relativeOrientationSensor, ts_relativeOrientationSensor);
    });

    accelerometer.start();
    gravitySensor.start();
    gyroscope.start();
    linearAccelerationSensor.start();
    //magnetometer.start();
    absoluteOrientationSensor.start();
    relativeOrientationSensor.start();
}

function numberFormat(n, places) {
    return (n < 0 ? "" : "+") + Number(n).toFixed(places);
}

function sensorReadXYZ(sensor, i, data, info, lastTimestamp) {
    if (sensor.timestamp == null || sensor.timestamp == lastTimestamp) {
        return;
    }
    lastTimestamp = sensor.timestamp;
    i++;
    data.push({
        Timestamp: sensor.timestamp,
        X: sensor.x,
        Y: sensor.y,
        Z: sensor.z
    });
    if (i == info_show_delay) {
        i = 0;
        info.textContent = `x: ${numberFormat(sensor.x, 5)} | y: ${numberFormat(sensor.y, 5)} | z: ${numberFormat(sensor.z, 5)}`
    }
    return i;
}

function sensorReadXYZW(sensor, i, data, info, lastTimestamp) {
    if (sensor.timestamp == null || sensor.timestamp == lastTimestamp) {
        return;
    }
    lastTimestamp = sensor.timestamp;
    i++;
    data.push({
        Timestamp: sensor.timestamp,
        X: sensor.x,
        Y: sensor.y,
        Z: sensor.z,
        W: sensor.w
    });
    if (i == info_show_delay) {
        i = 0;
        info.textContent = `x: ${numberFormat(sensor.quaternion[0], 3)} | y: ${numberFormat(sensor.quaternion[1], 3)} | z: ${numberFormat(sensor.quaternion[2], 3)} | w: ${numberFormat(sensor.quaternion[3], 3)}`
    }
    return i;
}

function stopCollecting() {
    gyroscope.stop();
}

function getCollectedData() {
    return JSON.stringify({
        Accelerometer: data_accelerometer,
        GravitySensor: data_gravitySensor,
        Gyroscope: data_gyroscope,
        LinearAccelerationSensor: data_linearAccelerationSensor,
        AbsoluteOrientationSensor: data_absoluteOrientationSensor,
        RelativeOrientationSensor: data_relativeOrientationSensor
    });
}