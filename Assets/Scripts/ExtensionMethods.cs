///ORG: Ghostyii & MoonLight Game

static public class ExtensionMethods 
{
    //get the reverse action for undo
    //获取反向操作，用于撤销
    static public MoveDirection Reverse(this MoveDirection d)
    {
        switch (d)
        {
            case MoveDirection.Left:
                return MoveDirection.Right;
            case MoveDirection.Right:
                return MoveDirection.Left;
            case MoveDirection.Up:
                return MoveDirection.Down;
            case MoveDirection.Down:
                return MoveDirection.Up;
            default:
                return MoveDirection.None;
        }
    }
}
