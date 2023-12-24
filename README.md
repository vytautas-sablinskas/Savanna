# Savanna

## Description
Savanna is a simulation game featuring a dynamic ecosystem with two types of animals: Predators and Preys. Each animal in the game possesses unique characteristics like special skills, health points (HP), vision range, damage output, and behavior. The game offers a rich and customizable environment where players can introduce their own animal species and behaviors through plugins.

## Project Structure
- **Savanna.ConsoleDisplay:** Manages the frontend interface for navigation and game initiation.
- **Savanna.GameLogic:** Contains all the core game logic, exclusive to the game and not shared with the plugin system.
- **Savanna.Data:** Provides data models and resources shared between the Savanna.GameLogic and Savanna.Plugins projects.
- **Savanna.Plugins:** Includes examples and the framework for creating custom animal plugins, which can be integrated into the game.
- **Savanna.Tests:** Comprises unit tests for the game, utilizing the Moq framework.

## Key Features
- Diverse Animal Types: Includes both predators and prey, each with distinct skills and behaviors.
- Customizable Behaviors and Skills: Animals have predefined behaviors like running away or chasing, and skills like invisibility or speed boosts. Players can create unique behaviors and skills through plugins.
- Dynamic Ecosystem: Interaction between animals leads to natural outcomes like hunting or fleeing. Animals of the same species can reproduce under certain conditions.
- Plugin System: Allows players to design their own animals, complete with custom behaviors and skills.

## Installation
Download and run the Savanna game executable. Ensure compatibility with the plugin system if you plan to use custom animal plugins.

## Usage
Upon launching Savanna, users are presented with three options:

1. Start Game: Start the simulation. If you have animal plugins, you will be prompted to select a .dll file; otherwise, press enter to proceed without plugins.
2. Change Board Size: Adjust the dimensions of the simulation environment. The default size is 20x20.
3. Exit Application: Close the game.

### In-Game Mechanics:
- Animal Interaction: Animals interact based on their type, behavior, and skills. Predators chase prey, while prey animals may flee or use defensive skills.
- Reproduction: If two animals of the same species are in proximity for three rounds, they reproduce, creating a new animal of the same type.
- Customization through Plugins: Players can create and add new animal species and behaviors to the game using the plugin system.

## Plugin Development
To develop custom plugins:
1. Build the plugin project to create your animals and behaviors.
2. Integrate the plugin with the main game to see your creations come to life in the Savanna ecosystem.
