using Script.Runtime.Enum;

namespace Script.Runtime.Signal
{
    public readonly struct ShowUIPanelSignal
    {
        public readonly UIPanelType PanelType;
        
        public ShowUIPanelSignal(UIPanelType panelType)
        {
            PanelType = panelType;
        }
    }
}