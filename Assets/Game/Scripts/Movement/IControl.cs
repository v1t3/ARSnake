namespace Game.Scripts.Movement
{
    public interface IControl
    {
        public void MoveUp();
        public void MoveDown();
        public void MoveLeft();
        public void MoveRight();
        public void SetNormalSpeed();
        public void SetFastSpeed();
    }
}