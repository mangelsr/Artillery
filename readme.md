# Artillery

A small artillery-style game prototype built with **Unity**.  
The project focuses on classic artillery gameplay: tweaking angle and power to launch projectiles across a level and hit targets.

---

## 🎮 Overview

**Artillery** is a learning / prototype project created to explore:

- 2D/3D projectile motion
- Basic physics-based gameplay (angle + power shooting)
- Simple level setup with targets/obstacles
- Unity rendering and shaders (custom materials / effects)

The repository is a standard Unity project with code primarily in **C#** and visual effects in **ShaderLab/HLSL**.  

---

## ✨ Key Features

- **Projectile-based combat**  
  Aim and shoot by adjusting launch parameters (angle, power, etc.).

- **Physics-driven trajectory**  
  Uses Unity’s physics to simulate realistic arcs for shells/projectiles.

- **Targets & obstacles**  
  Simple targets to hit and optional obstacles to shoot over or around.

- **Camera & view**  
  A basic camera setup to frame the artillery and impact area.

- **Extensible gameplay**  
  Code and project structure are organized so it’s easy to add:
  - New projectile types (grenades, rockets, etc.)
  - Wind or other environmental effects
  - Different weapons or firing behaviors

---

## 🧱 Project Structure

At a high level, the repo follows a typical Unity layout:​:contentReference[oaicite:1]{index=1}  

- `Assets/`  
  - **Scenes** – Main game scene(s) for the artillery prototype.  
  - **Scripts** – C# scripts for artillery control, projectile logic, input, and game flow.  
  - **Materials / Shaders** – Custom shaders and materials (ShaderLab/HLSL) used for visual effects.  
  - **Prefabs** – Artillery, projectiles, and target prefabs.

- `ProjectSettings/`  
  Unity project configuration (input, graphics, quality settings, etc.).

- `Packages/`  
  Unity package configuration, managed via Unity Package Manager.

- `.vscode/`  
  Optional editor configuration for VS Code.:contentReference[oaicite:2]{index=2}  

- `Doxyfile`  
  Configuration file for generating API documentation with **Doxygen** (if you want to document the C# scripts).:contentReference[oaicite:3]{index=3}  

- `notes.md`  
  Internal notes / design ideas for the project.

---

## 🧰 Requirements

- **Unity** – Any recent Unity version compatible with the project (ideally an LTS release).  
- **Git** – (Optional) To clone the repository.  
- **Doxygen** – (Optional) If you want to generate code documentation from `Doxyfile`.

---

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mangelsr/Artillery.git
cd Artillery
