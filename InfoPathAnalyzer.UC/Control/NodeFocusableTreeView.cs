using System;
using System.Drawing;
using System.Windows.Forms;

namespace GeekBangCN.InfoPathAnalyzer.UC.Control
{
    public class NodeFocusableTreeView : TreeView
    {
        public NodeFocusableTreeView()
        {
            this.DrawMode = TreeViewDrawMode.OwnerDrawText;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            TreeNodeStates state = e.State;
            Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
            Color fore = e.Node.ForeColor;
            if (fore == Color.Empty)
                fore = e.Node.TreeView.ForeColor;
            if (e.Node == e.Node.TreeView.SelectedNode)
            {
                fore = SystemColors.HighlightText;
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, fore, SystemColors.Highlight);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, fore, TextFormatFlags.GlyphOverhangPadding);
            }
            else
            {
                Brush b = new SolidBrush(e.Node.BackColor);
                e.Graphics.FillRectangle(b, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, fore, TextFormatFlags.GlyphOverhangPadding);
            }
        }
    }
}
