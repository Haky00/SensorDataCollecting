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

function sensorData(infoDiv) {
    return {
        data: [],
        i: 0,
        ts: 0,
        info: document.getElementById(infoDiv)
    };
}

function startCollecting() {
    data_accelerometer = sensorData("accelerometer-info");
    data_gravitySensor = sensorData("gravitySensor-info");
    data_gyroscope = sensorData("gyroscope-info");
    data_linearAccelerationSensor = sensorData("linearAccelerationSensor-info");
    //data_magnetometer = sensorData("magnetometer-info");
    data_absoluteOrientationSensor = sensorData("absoluteOrientationSensor-info");
    data_relativeOrientationSensor = sensorData("relativeOrientationSensor-info");

    data_accelerometer.info.textContent = "Starting...";
    data_gravitySensor.info.textContent = "Starting...";
    data_gyroscope.info.textContent = "Starting...";
    data_linearAccelerationSensor.info.textContent = "Starting...";
    //data_magnetometer.info.textContent = "Starting...";
    data_absoluteOrientationSensor.info.textContent = "Starting...";
    data_relativeOrientationSensor.info.textContent = "Starting...";

    accelerometer = new Accelerometer({ frequency: 60 });
    accelerometer.addEventListener("reading", (e) => {
        sensorReadXYZ(accelerometer, data_accelerometer);
    });

    gravitySensor = new GravitySensor({ frequency: 60 });
    gravitySensor.addEventListener("reading", (e) => {
        sensorReadXYZ(gravitySensor, data_gravitySensor);
    });

    gyroscope = new Gyroscope({ frequency: 60 });
    gyroscope.addEventListener("reading", (e) => {
        sensorReadXYZ(gyroscope, data_gyroscope);
    });

    linearAccelerationSensor = new LinearAccelerationSensor({ frequency: 60 });
    linearAccelerationSensor.addEventListener("reading", (e) => {
        sensorReadXYZ(linearAccelerationSensor, data_linearAccelerationSensor);
    });

    //magnetometer = new Magnetometer({ frequency: 60 });
    //magnetometer.addEventListener("reading", (e) => {
    //    sensorReadXYZ(magnetometer, data_magnetometer);
    //});

    absoluteOrientationSensor = new AbsoluteOrientationSensor({ frequency: 60, referenceFrame: "device" });
    absoluteOrientationSensor.addEventListener("reading", (e) => {
        sensorReadXYZW(absoluteOrientationSensor, data_absoluteOrientationSensor);
    });

    relativeOrientationSensor = new RelativeOrientationSensor({ frequency: 60, referenceFrame: "device" });
    relativeOrientationSensor.addEventListener("reading", (e) => {
        sensorReadXYZW(relativeOrientationSensor, data_relativeOrientationSensor);
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

function sensorReadXYZ(sensor, data) {
    if (sensor.timestamp == null || sensor.timestamp == data.ts) {
        return;
    }
    data.ts = sensor.timestamp;
    data.i++;
    data.data.push({
        Timestamp: sensor.timestamp,
        X: sensor.x,
        Y: sensor.y,
        Z: sensor.z
    });
    if (data.i == info_show_delay) {
        data.i = 0;
        data.info.textContent = `x: ${numberFormat(sensor.x, 5)} | y: ${numberFormat(sensor.y, 5)} | z: ${numberFormat(sensor.z, 5)}`
    }
}

function sensorReadXYZW(sensor, data) {
    if (sensor.timestamp == null || sensor.timestamp == data.ts) {
        return;
    }
    data.ts = sensor.timestamp;
    data.i++;
    data.data.push({
        Timestamp: sensor.timestamp,
        X: sensor.quaternion[0],
        Y: sensor.quaternion[1],
        Z: sensor.quaternion[2],
        W: sensor.quaternion[3]
    });
    if (data.i == info_show_delay) {
        data.i = 0;
        data.info.textContent = `x: ${numberFormat(sensor.quaternion[0], 3)} | y: ${numberFormat(sensor.quaternion[1], 3)} | z: ${numberFormat(sensor.quaternion[2], 3)} | w: ${numberFormat(sensor.quaternion[3], 3)}`
    }
}

function stopCollecting() {
    gyroscope.stop();
}

function getCollectedData() {
    return JSON.stringify({
        Accelerometer: data_accelerometer.data,
        GravitySensor: data_gravitySensor.data,
        Gyroscope: data_gyroscope.data,
        LinearAccelerationSensor: data_linearAccelerationSensor.data,
        AbsoluteOrientationSensor: data_absoluteOrientationSensor.data,
        RelativeOrientationSensor: data_relativeOrientationSensor.data
    });
}