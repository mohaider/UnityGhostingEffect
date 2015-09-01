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
4. ~~The duration of the effect: how long do you want the effect to last for?~~ **No longer in use because I had to change the algorithm. This was causing problems as the effect wasn't lining up properly with the number of ghosts on screen but again. I'm not removing this parameter as some people are still using this function and don't want to break their code. I appologize for the inconvienience.**
5. (Optional ) pass in a color parameter to set the ghosts to a different color. 
 


## Contributing
1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D
