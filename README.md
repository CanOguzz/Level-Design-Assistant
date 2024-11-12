# Level-Design-Assistant
Level design assistant tool for the Unity game engine



A custom Unity Editor tool that procedurally generates terrain using Perlin Noise is designed to assist level designers in creating diverse, adjustable terrains quickly and efficiently. Created as a demonstration of skills in Unity, C# scripting, and procedural generation.

Project Overview
Level Design Assistant is a Unity Editor extension tool that generates procedurally generated terrains with customizable parameters, such as terrain size, height, noise scale, and more. This project showcases foundational skills in Unity editor scripting and procedural content generation, aligned with game development practices.

Features
Procedural Terrain Generation: Generates realistic terrain using Perlin Noise.
Customizable Parameters: You can adjust the terrain size, height, noise scale, seed, octaves, persistence, and lacunarity.
Unity Editor Integration: Accessible via a custom editor window, providing a streamlined workflow for level designers.
Seed-Based Randomization: Enables reproducible terrain results.
Technologies Used
Unity
C#: For editor scripting and procedural generation
Universal Render Pipeline (URP): Optional (ensure URP settings for compatible materials)



Using the Tool? 

Open Unity and load the project.
Navigate to the Level Design Assistant:
In the top menu bar, go to Tools > Level Design Assistant to open the custom editor window.
Parameter Adjustments
Each parameter in the Level Design Assistant window provides control over terrain generation:

Terrain Width & Length: Sets the dimensions of the terrain.
Terrain Height: Determines the maximum elevation of the terrain.
Noise Scale: Controls the scale of the Perlin Noise pattern (lower values create smoother terrain).
Seed: A seed value for randomization (useful for reproducibility).
Octaves: Number of noise layers for added detail.
Persistence: Adjusts the impact of each octave.
Lacunarity: Controls frequency scaling between octaves.
Generating Terrain
Adjust Parameters as desired.
Click "Generate Terrain" to create a procedurally generated terrain in the Scene view.
Experiment with Parameters for varied terrain styles.

Future Enhancements:


Advanced AI Integration: Incorporate machine learning algorithms to generate specific types of terrain based on user input.
Erosion Simulation: Add erosion and sediment simulation for more realistic terrain shaping.
Real-Time Preview: Provide real-time terrain preview as parameters are adjusted.
Multi-Biome Support: Allow multiple terrain layers to simulate biomes like forests, mountains, and deserts.

Known Issues
URP Compatibility: Ensure URP settings are configured correctly for proper material rendering.
Unity Version Compatibility: Tested primarily in Unity 2021.3 LTS. Some features may vary in other versions.

Contact
If you have any questions or feedback, feel free to reach out:

Email:mustafacanoguz@gmail.com
LinkedIn: https://www.linkedin.com/in/mustafa-can-oguz-9922b3222/
