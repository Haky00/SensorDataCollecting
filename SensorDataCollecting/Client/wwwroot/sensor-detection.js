async function sensorsAvailable() {
    if (
        window.AbsoluteOrientationSensor &&
        window.Accelerometer &&
        window.GravitySensor &&
        window.Gyroscope &&
        window.LinearAccelerationSensor &&
        //window.Magnetometer &&
        window.RelativeOrientationSensor) {

        if (await sensorPermission("gyroscope") && await sensorPermission("accelerometer") && await sensorPermission("magnetometer")) {
            return true;
        }
    }

    return false;
}

function sensorPermission(sensorName) {
    return new Promise((resolve) => {
        navigator.permissions.query({ name: sensorName }).then((result) => {
            if (result.state === "denied") {
                console.log("Permission to use " + sensorName + " is denied.");
                resolve(false);
            }
            resolve(true);
        });
    });
}
