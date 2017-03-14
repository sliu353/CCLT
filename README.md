# CCLT

This application is designed for solving a real-life problem in chemical industry, which is getting a certain amount of mixture of a certain density by mixing the available Materials of different density. A detailed description of this problem as follows:

Supposing we have n different materials available (M1, M2, ..., Mi, ..., Mn), the units and density of which are Ui and Di where i represent the index of the material, now we want to take a certain units of each materials (ui) and mix them into m units of mixture of density between λ (the lowest acceptable density) and η (the highest acceptable density), which can be represented as formula: 

<div style="text-align:center"><img src ="/Formula.PNG" /></div>

As the fomulas suggest, there can always be multiple solutions to this formula system. Without software supporting, to find a solution, we have to attemp a guess to see if our guess fullfill the all the formulas, if not we have to make a new guess by adjusting our previous guess and retry, and keep doing so until we make the correct attempt. However, solving this problems in this way is obviously not efficient, problematic, time-consuming and even almost impossible when the number of materials goes larger (let's say we have ten materials :( ).

My program can make life much easier when user uses it to solve this kind of problems. 

To begin with, at the starting page, my program first requires user to enter the name, the number of available units and the density (MV) of the all the materials they want to mix and the maximum acceptable density, the minimum acceptable density and required units of the mixture we want. 

<div style="text-align:center"><img src ="/Formula.PNG" /></div>

Then after user submit those information, the program will find a highly possibly correct solution in a greedy approach (In some edge cases the solution the program finds is not correct but very close to the correct solutions) and the solution would be displayed by several radial sliders, each of which represents a material, and a number at the top of the page representing the density of the mixture. By selecting two of the radial sliders that they want to adjust and sliding one of them, the other one will change in a contradict direction, which is like you can increase one while decrease the other one and vice versa. In this way user can easily make adjustments and finally find the best way that they want to get the mixture using the available materials.

<div style="text-align:center"><img src ="/Formula.PNG" /></div>
<div style="text-align:center"><img src ="/Formula.PNG" /></div>

This app is available at https://1drv.ms/f/s!Agy2cxjV1wOXkFE5pMASlFfQFVQE
