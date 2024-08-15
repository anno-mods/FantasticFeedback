# FantasticFeedback

- Positioning Dummies happens in Blender.
- Export the Dummies
- Import the fc file
- Create your Feedback Loops with a Node based Editor
- Export the entire Thing

# THIS EDITOR IS NOT PRODUCTION-READY UNDER ANY CIRCUMSTANCE

# What it can currently do
- Import Fc Files converted with FileDBReader (needs interpretation using FcFile.xml)
- Export into that format
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
- Manipulate and Add Sequence Elements in existing loops, for existing actors.

# Planned Features
- Controlling the `[Root Object]`
- Supporting all other Element Types. I know that those two also exist, which will be implemented with all the spline stuff:
  - `Walk Spline (4)`
  - `Follow Spline Any Walk Sequence (15)`
- This also implies there is 5,8,10,11,13,14 which I have not encountered so far. If you encounter one of them in your fc files please let me know, including info in which vanilla file you spotted it.


# Credits

- [NodeNetwork by Wouterdek](https://github.com/Wouterdek/NodeNetwork)
- [Mona04s Modification](https://github.com/Mona04/WPF-Timelines) of [DannyStaten's Timeline Control](https://www.codeproject.com/Articles/240411/WPF-Timeline-Control-Part-I)
- [Stopbytes Numeric Spinner](https://github.com/Stopbyte/WPF-Numeric-Spinner-NumericUpDown/)
