# CCLT

This application is designed for solving a real-life problem in chemical industry, which is getting a certain amount of mixture of a certain density by mixing the available Materials of different density. A detailed description of this problem as follows:

Supposing we have n different materials available (M1, M2, ..., Mi, ..., Mn), the units and density of which are Ui and Di where i represent the index of the material, now we want to take a certain units of each materials (ui) and mix them into m units of mixture of density between λ (the lowest acceptable density) and η (the highest acceptable density), which can be represented as formula: 

<div style="text-align:center"><img src ="/Formula.PNG" /></div>

As the fomulas suggest, there can always be multiple solutions to this formula system. Without software supporting, to find a solution, we have to attemp a guess to see if our guess fullfill the all the formulas, if not we have to make a new guess by adjusting our previous guess and retry, and keep doing so until we make the correct attempt. However, solving this problems in this way is obviously not efficient, problematic, time-consuming and even almost impossible when the number of materials goes larger (let's say we have ten materials :( ).

My program can make life much easier when user uses it to solve this kind of problems. Basically my program first requires user to enter the name, the number of available units and the density (MV) of the all the materials they want to mix and  
