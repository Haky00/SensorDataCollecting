# Sensor Data Collecting

This project is a part of a master's thesis "Movement simulation of a handheld device". Related repositories can be found [here](https://github.com/stars/Haky00/lists/dp).

## About

A web app for collecting movement sensor data using the Generic Sensor API. Only Google Chrome and other Chromium-based browsers currently grant access to device sensors through this API.

Blazor WASM is used to create the app. Instructions for using the app are found on the home page. Collected data is sent to a database for analysis and usage for the purposes of the master's thesis.

The app is hosted using GitHub Pages and can be found [here](https://haky00.github.io/SensorDataCollecting/). Supabase (free tier) is used as the database provider. Because of this, the project may be paused if no activity is detected for a period of time - in this case, uploading will not be successful. You can contact me if you need the project unpaused.