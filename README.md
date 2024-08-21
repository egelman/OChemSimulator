Nikhil Vishwanath

<img src = "https://github.com/csci48306830fa23/project-2-virtual-vanguard/blob/53cdded65707e32fde6386b6c3b3db130e849132/IMG_3553.jpg" width="100">  

Kasra Ghaffari

<img src = "https://github.com/csci48306830fa23/project-2-virtual-vanguard/blob/53cdded65707e32fde6386b6c3b3db130e849132/IMG_8112.jpg" width="100">  

Sohan Shankar

<img src="https://github.com/csci48306830fa23/project-1-seaa/assets/119974810/fb9cb79a-220a-4cc6-8f47-b5eb7378d50e" width="100">  


Eliana Gelman  
<img src="https://github.com/csci48306830fa23/project-1-seaa/blob/main/vr-project1/Gelman-Eliana.jpg?raw=true" width="100">  

For our VR final, we created a virtual classroom as an instructional tool for organic chemistry education. The multiplayer functionality means multiple people can be within the simulation to assemble carbon-based molecules collaboratively, quiz each other on their molecular knowledge, and conduct lab practicals in the scene. We also implemented haptics through the Novint Falcon, a resistance knob that allows users to feel the molecular mass of the molecule they create in the simulation. There is also live feedback about the molecule type created and property data about the molecule displayed on whiteboards around the classroom. This project can scale to accommodate more than complex carbon chains, access internet data on compounds created, and make more complex lab practicals. 

Multiplayer configuration is always difficult and proved especially so when integrating the Novint. Most of the documentation is from 2008 since that's when it was released and hadn't been updated since 2011, and even finding the drivers was very hard. Once we had it working in single-player and the player was "in possession" of the built molecule and able to control it,  the networking messed it up in multiplayer so that took a while to figure out. Also, building the molecules proved incredibly difficult because of storing all of the connections, not relying on Unity physics to keep the molecules together, calculating the proper rotations, and traversing the graph to name and identify the molecules created.


[Our Demo Video](https://youtu.be/c2vYxqtZjlI)
[![Our Demo Video](https://github.com/csci48306830fa23/project-2-virtual-vanguard/blob/c53c3124043e1bee61c334df0a0b1a2337cbe004/Screenshot%202023-12-12%20at%2011.30.51%20PM.jpg?raw=true)](https://youtu.be/c2vYxqtZjlI)

