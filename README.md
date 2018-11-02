# Ong-Sharing-Drag-n-Drop

Tested in Unity 2018.1.9f1 and 2018.1.9f2

Instructions for Legacy MRTK:

1. Import the OngDragDropSharing.unitypackage into project

2. If asked, please enter in the following Photon code: 8bdcbdce-1ef6-4860-9e27-953ba9d52f88
(Note - this is a test Photon ID code to use. You will need to obtain your own Photon code for apps)

3. Drag and drop the "OngSharingDragDrop" prefab (located within the OngSharing folder) into your scene

4. Drag and drop anything in your scene that you want to share as a child of the OngSharingDragDrop prefab in your scene. 

Your app is now a shared experience! All actions will be shared. 
Note: Sharing of app states is not yet supported.

To enable real-time movement syncing of objects:

1. On the object you want to sync movements, drag and drop (or insert) the PhotonTransformView script
2. Select any or all syncing features (position, rotation, scale)
3. Drag the PhotonTransformView script into the "Observed Components" empty field (it will automatically be generated on your object)
4. In the PhotonView script (it was automatically generated) - select the dropdown list and change it from "Fixed" to "Takeover" - this allows all people to takeover ownership, rather than just letting ownership of that object be fixed to the first person who joined.



