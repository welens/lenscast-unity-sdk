# LensCast Unity SDK

The LensCast SDK is a toolset for building AR/VR Unity apps that can be remotely managed and monitored via a companion mobile app for iOS and Android phones and tablets.

The companion remote control app is useful both for one-on-one screenings of interactive room-scale VR and group screenings of VR or AR narrative content.

## Managing Devices with LensCast 

### Configuring  Remote Control Properties 

To customize the functionality of the LensCast remote control app, a  *lenscast.yaml* file is added to your application project used to describe the actions available for your app.

Actions consist of a name, an identifier, and a theme that is used to style the corresponding remote control button (primary, default, warning).  Actions can also have optional values. The number of values specified changes the form field type of the action as follows:

* No values or one value - the action will be rendered as a simple button. If no values are specified, the value object sent via the SDK will be identical to the action object.
* Two values - the action will be rendered as a toggle switch.
* Three or more values - the action will be rendered as a multiple choice dropdown form field.


### Property File Examples

```
title: Example Video Player App
description: Description of the example video player application.
actions:
  action: 
    id: play
    name: Play
    theme: primary 
  action: 
    id: stop
    name: Stop
    theme: default
  action:
    id: select_video
    name: Select Video 
    theme: default 
    values:
       value:
          id: 1
          name: First Video

```


```
title: Example Game or Interactive App
description: Description of the example application.
actions:
  action: 
    id: reset_level
    name: Reset Level
    theme: primary 
    action:
      id: change_difficulty
      name: Change Difficulty
      theme: warning
      value:
        id: easy
        name: Easy Mode
    action:
      id: change_level
      name: Change Level
      theme: default
      value:
        id: 1
        name: Level 1

```

### Sending Remote Control Commands

Once the LensCast app is open on the remote control device and the client application is open on nearby devices, the devices automatically attempt to establish connectivity using WiFi or BLE. The remote control app lists any found devices and their current status. 

The remote control app also renders buttons and options that have been configured with the application’s *lenscast.yaml* file. 

The remote control app is then used to send commands to either all listed playback devices, or one selected playback devices, containing the action and value selected by the user of the remote control app. 

### Handling Remote Control Commands


The client application attaches a delegate function to the *LensCast.onRemoteCommand* event. The delegate function can inspect the actions and values are contained by the command, and trigger specific changes in the app. 

The properties of the action and value objects correspond to those defined in the *lenscast.yaml* file. 

Example:
```
void Start() {
 // This code only needs to be run once when the app is initialized
 LensCast.OnRemoteCommand += RemoteCommandHandler;
}

void RemoteCommandHandler(LensCast action, int timestamp)
    {
      if (action.id == "set_level"){
          SetLevel(action.value.id);
      }
      if (action.id == "set_difficulty"){
	  SetDifficulty(action.value.id);
      }
    }
```


## Monitoring Devices with LensCast

There is no configuration required for devices to periodically sent status information to the remote control device where it is displayed in the LensCast app.  This status information includes battery, temperature, orientation and accelerometer data. Using the *LensCast.setOption* method, various options for this monitoring functionality can be configured.

### Disabling or modifying monitoring 

The `enableMonitoring` option accepts a boolean value to enable or disable monitoring at any time.

```
LensCast.setOption("enableMonitoring", false); 
```

The rate at which monitoring is sent can be modified. The `monitoringRate` option accepts a number of seconds to wait before sending the next batch of status information.

For example, to send status information every 5 seconds:

```
LensCast.setOption("monitoringRate", 5); 
```

The type of status information can also be customized by passing an area of status type strings to the `monitoringData` option:

```
LensCast.setOption("monitoringData", ["battery","orientation"]); 
```


### Setting Device Status 

The LensCast SDK can be used to manually update the status of the device, which will update their appearance in the remote control view of the LensPass Android app. 

Supported statuses include: `default`, `primary`, `success`, and `warning`. An optional message can be included with the status. 

The status is set using the *LensCast.setStatus* method:

```
LensCast.setStatus("success", "Playing Video #1") ;
```

This LensCast.setStatus method can be used in the *LensCast.onRemoteCommand* delegate function  to send a verification that a command sent from the remote control app has been processed:

```
void RemoteCommandHandler(LensAction Action, int timestamp)  {
      if (action.id == "stop"){
	StopVideo();
        LensCast.setStatus("default", "Ready to play next video") ;
      }
    }
```



