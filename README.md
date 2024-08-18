# FantasticFeedback

- Positioning Dummies happens in Blender.
- Export the Dummies
- Import the fc file or just the Dummies.
- Create your Feedback Loops with a Node based Editor
- Export the entire Thing

![grafik](https://github.com/user-attachments/assets/4df3e507-0f26-4b4c-a305-228f005cd388)

# Requirements 

- dotnet 6.0 Runtime
- [FileDBReader, at least Version 3.0.3](https://github.com/anno-mods/FileDBReader/releases/latest) if you want to import Fc Files directly.

# What it can do
- Import Fc Files, either directly or the FileDBReader converted variant.
- Export into those formats format
- Add and remove Actors
- Add and Remove Sequences
- Supports ElementTypes:.
  - `Walk (0)`
  - `Play Sequence (1)`
  - `Wait (2)`
  - `Branch (3)`
  - `Fade (6)`
  - `Barrier (7)`
  - `Scale (9)`
  - `Play Any Sequence (12)`
  - **All other Sequences and their data are currently lost, except for their Element Type when doing an import.**
- Manipulate and Add Sequence Elements in Loops
- 

# Planned Features
- Supporting all other Element Types. I know that those two also exist, which will be implemented with all the spline stuff:
  - `Walk Spline (4)`
  - `Follow Spline Any Walk Sequence (15)`
- This also implies there is 5,8,10,11,13,14 which I have not encountered so far. If you encounter one of them in your fc files please let me know, including info in which vanilla file you spotted it.

# Credits

- [NodeNetwork by Wouterdek](https://github.com/Wouterdek/NodeNetwork)
- [Mona04s Modification](https://github.com/Mona04/WPF-Timelines) of [DannyStaten's Timeline Control](https://www.codeproject.com/Articles/240411/WPF-Timeline-Control-Part-I)
- [Stopbytes Numeric Spinner](https://github.com/Stopbyte/WPF-Numeric-Spinner-NumericUpDown/)
- [Lion053](https://github.com/lion053) for extensive Experimentation on fc files, which was enormously helpful when building this editor.
