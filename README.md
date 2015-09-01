# UnityGhostingEffect
A visual effect for unity that leaves a series of transparent copies of sprites behind

## How do I install it? 
Download the files and open the project in Unity or place them in your own project. Just make sure to leave the Resources folder intact
## How do I use it?
 Attach `GhostingContainer.cs` to a game object that contains a SpriteRenderer.
 To start the ghost effect pass in these parameters to the  GhostingContainer's init function
 
1. The maximum number of ghosts, of course the more you want the bigger the hit on game performance
2. Spawning rate: the rate at which the "ghosts" spawn behind your target
3. The sprite renderer of the target in question.
4. The duration of the effect: how long do you want the effect to last for?
5. (Optional ) pass in a color parameter to set the ghosts to a different color. 
Frankly, I didn't like this effect too much and only implemented this functionality after I found a shader that changed the sprite's color. I might remove this sometime.


## Contributing
1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D
