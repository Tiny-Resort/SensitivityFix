Makes the camera movement more consistent and smooth regardless of your FPS. This will be especially noticeable on gamepad, but improves the experience with a mouse as well. 

**More detailed explanation**

In the base game, camera movement is done each frame and is based on the received input and the look sensitivity. All of this is multiplied by a constant 0.008, about half the time it takes for one frame at 60FPS. The problem is that if the game's FPS drops below 60, then this calculation is being done less often. The result is that the camera feels sluggish and inconsistent if your framerate drops or fluctuates (a frequent problem with a lot of mods). Not only is this frustrating when trying to turn around, it also makes the game feel laggier than it is.

My fix was to replace the constant 0.008 modifier with the half amount of time that has passed since the previous frame. This means that if your framerate gets lower, the sensitivity is boosted to compensate. The result is a more consistent and smooth look sensitivity, and smoother-feeling gameplay.