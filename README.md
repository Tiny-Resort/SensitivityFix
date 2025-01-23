Smoothes out the camera movement speed when experiencing inconsistent FPS. This will be especially noticeable on gamepad, but improves the experience with a mouse as well. 

##Important Notes
* You will probably need to adjust your look sensitivity slightly the first time you play after installing this. It may be slightly slower or faster than you're used to depending on your typical FPS. This is especially true if you usually play at higher than 60FPS.
* This mod has no impact on performance and will not reduce your FPS. It will make the camera (and thus the game) feel significantly less sluggish. However, the camera being smoother also means it'll be a little more obvious when the game is rendering less frames (low FPS). To get the best balance, this mod will allow the camera speed to slow down if your FPS is lower than 30.

##How it works
In the base game, camera movement is set to move a constant amount each frame. If your FPS is lower than 60, the camera moves slower. If it's higher than 60, the camera moves faster. That's fine if your FPS is consistent, but lethal company mods can result in very inconsistent FPS. One moon might have a smooth 60fps, while another constantly dips below 30-40fps for any number of reasons. Because of this, the camera movement speed feels much slower on some moons than on others.

I fix this issue by making the camera speed depend on how much time has passed since the last frame. **So, this mod will compensate for changes in FPS, resulting in a much smoother and more consistent camera speed.**

##Technical explanation
The base game multiplies the look sensitivity by 0.008, which is approximately half the time that passes between frames when you're playing at 60FPS. Since it's a constant value, the more frames you have, the further your camera moves over a given second. This mod changes that constant value to one that depends on the time that's passed since the last frame. 

I chose to divide the delta time by 2 so that if you're playing at 60 FPS, the resulting value is very close to the original constant of 0.008. That way, installing this mod generally results in very little difference in look sensitivity from what you're used to.

With the camera being smoother, especially low framerates can be more visually noticeable. Also, if there is a sudden intense lag spike, the camera would otherwise move really far, looking strange. So, I also clamp the delta time value used to a minimum of 0.0083. This means that below 30 FPS, it will use the sensitivity that it would at 30 FPS.