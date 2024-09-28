using Script.Runtime.Enum;

namespace Script.Runtime.Signal
{
    public readonly struct HideUIPanelSignal
    {
        public readonly UIPanelType PanelType;
        
        public HideUIPanelSignal(UIPanelType panelType)
        {
            PanelType = panelType;
        }
    }
}