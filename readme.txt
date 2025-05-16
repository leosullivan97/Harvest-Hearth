Group Name: Bario Studios

Game Name: Harvest Hearth

Game Description:
---------------------------
Harvest Hearth is a cozy fantasy farming simulator where players start their own farm, grow crops, and sell goods to the townsfolk to become part of the local community. The game features a day and night cycle that paces gameplay and sets a timeline for crop growth. Players can also acquire farm animals to produce alternative goods as well as fish to sell and expand their farm.

The game includes a variety of tools, each serving a unique purpose—tilling the land, harvesting crops, and caring for animals. Players can purchase seeds, tools, and animals from the shop and manage them through an inventory system.

A CS 596 concept featured in Harvest Hearth is the implementation of a state machine to manage the behavior and interactions of farm animals. Animals have a hunger status, which affects how they move and how the player can interact with them. Depending on their hunger level, they may walk slowly, move faster, or stop entirely. If time allows, we also plan to implement a custom 2D shader, such as animated fish effects, to enhance the visual atmosphere.

Controls:
---------------------------

Movement
  - W / A / S / D or Arrow Keys     Move the player character
  - Left / Right movement           Flips the player sprite based on direction

Tool Use
  - Left Click, E, or Space         Use selected tool (e.g., plough, water, plant, harvest)

Tool Switching
  - Tab                             Cycle through tools (Plough → Watering Can → Seeds → Basket)
  - 1 / 2 / 3 / 4 / 5               Direct tool selection:
      1 - Plough
      2 - Watering Can
      3 - Seeds
      4 - Basket
      5 - Fishing Rod

Seed Selection
  - Click seed in inventory UI      Switch to that seed for planting

Inventory & Shop
  - I                               Open/close player inventory
  - B                               Open/close shop (Editor only)
  - E or Space (near shop area)     Open shop

Pause Menu
  - Escape or P                     Toggle pause menu

Sleep / End Day
  - E / Space / Left Click (near bed)    End the day and trigger day transition

Mouse Interaction
  - Mouse Position                  Controls the tool indicator's position
  - Note: Tool range is limited; indicator snaps to grid and is clamped within range

Group Organization:
---------------------------
While all team members contributed collaboratively, here is an overview of completed tasks and upcoming responsibilities for each member:

Juan C
Completed: Fishing rod implementation and fishing minigame. Custom 2D shader for the rare fish. Water custom shader (not properly implemented in final version).

Kai C
Completed: Navmesh for the map. Chicken and state machine implementation. Final version of map.

Adam G
Completed: General tool management, including ploughing mechanic, watering can functionality, seed planting, and basket harvesting.

Leo S
Completed: Crop growth system, time mechanic, tillable land grid system, player inventory, currency system, and shop functionality. Player controls and animation. Scene transition.

Mark T
Completed: Art direction, sprite assets, main map prototype, player house interior, and camera functionality.