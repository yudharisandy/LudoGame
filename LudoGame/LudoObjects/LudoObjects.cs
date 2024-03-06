namespace LudoGame.LudoObjects;

public class Totem
{
    // + mathvector position : public get
    // + mathvector homePosition : public get
    // - List~int~ path
    // + IPlayer Owner : readonly
    // + void AdvanceOnce()
    // + void GoHome()
}
public class Cell
{
    // + CellType type : readonly
    // - List~Totem~ Occupants
    // + void AddTotem(Totem)
    // + bool KickTotem(Totem)
    // + IPlayer GetOwnership()
}
public enum CellType
{
    // <<enumeration>>
    // Normal
    // Safe
}
public class LudoDice
{
    // +int GetLastRoll()
    // +int Roll()
}
public class Board
{
    // +List~Cell~ Cells : readonly
    // +List~Path~ Paths : readonly
}