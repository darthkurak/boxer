using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpriteUtility.Stubs;

namespace SpriteUtility
{
    public partial class CustomSelection : UserControl
    {
        public event EventHandler<EventArgs> Selected;
        public event EventHandler<EventArgs> Expanded;
        public event EventHandler<EventArgs> Collapsed;
        public event EventHandler<EventArgs> Unselected;

        private bool m_Selected;
        private bool m_Expanded;
        private List<CustomSelection> m_Expandable;
        private CustomSelection m_Parent;

        public CustomSelectionPanel Panel { get; set; }

        public bool IsExpanded
        {
            get { return m_Expanded; }
        }

        public CustomSelection()
            : this(null)
        {

        }

        public CustomSelection(CustomSelection parent)
        {
            InitializeComponent();
            m_Parent = parent;
            m_Selected = false;
            m_Expanded = false;
            m_Expandable = new List<CustomSelection>();
            Selected += new EventHandler<EventArgs>(OnSelected);
            Unselected += new EventHandler<EventArgs>(OnUnselected);
            Expanded += OnExpanded;
            Collapsed += OnCollapsed;
        }

        private void OnCollapsed(object sender, EventArgs eventArgs)
        {
            int counter;

            for (counter = 0; counter < m_Expandable.Count; counter++)
            {
                ((CustomSelectionPanel)Parent).RemoveSelectable(m_Expandable[counter]);
            }

            m_Expanded = false;
        }

        private void OnExpanded(object sender, EventArgs eventArgs)
        {
            m_Expanded = true;
            int counter;
            int index;

            var expandable = m_Expandable;

            if (Expandable == null || Expandable.Count == 0)
                return;

            if (Expandable.First() is ImageStub || Expandable.First() is FolderStub)
            {
                expandable = m_Expandable.OrderBy(p =>
                {
                    if (p is ImageStub)
                        return (p as ImageStub).Image.Name;
                    if (p is FolderStub)
                        return (p as FolderStub).Folder.Name;

                    return "";
                }).ToList();
            }

            if (ParentSelection == null)
                index = 0;
            else
                index = ((CustomSelectionPanel) Parent).Controls.GetChildIndex(this);

            for (counter = 0; counter < expandable.Count; counter++)
            {
                index = ((CustomSelectionPanel)Parent).InsertSelectable(expandable[counter], index + 1, GetTreeLevel()+1);
            }
        }

        private int GetTreeLevel()
        {
            int level = 0;

            var parentSelection = ParentSelection;

            while (parentSelection != null)
            {
                parentSelection = parentSelection.ParentSelection;
                level++;
            }

            return level;
        }
        protected virtual void OnSelected(object sender, EventArgs e)
        {
            m_Selected = true;
            BackColor = Color.PowderBlue;
        }

        protected virtual void OnUnselected(object sender, EventArgs e)
        {
            m_Selected = false;
            BackColor = Color.White;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupControl(this);
        }

        private void ControlClicked(object sender, EventArgs e)
        {
            if (!m_Selected)
            {
                Selected(this, EventArgs.Empty);
            }
        }

        private void ControlDoubleClicked(object sender, EventArgs e)
        {
            if (!m_Expanded)
            {
                Expanded(this, EventArgs.Empty);
            }
            else
            {
                Collapsed(this, EventArgs.Empty);
            }
        }

        public bool IsSelected
        {
            get { return m_Selected; }
        }

        public List<CustomSelection> Expandable
        {
            get { return m_Expandable; }
        }

        public CustomSelection ParentSelection
        {
            get { return m_Parent; }
        }

        public void AddExpandable(CustomSelection sel)
        {
            m_Expandable.Add(sel);
            //var index = Panel.Controls.GetChildIndex(this);
            //if (m_Expanded)
            //    ((CustomSelectionPanel)Parent).InsertSelectable(sel, index+1);
        }

        public void RemoveExpandable(CustomSelection sel)
        {
            m_Expandable.Remove(sel);
            if (m_Expanded)
                ((CustomSelectionPanel)Parent).RemoveSelectable(sel);
        }

        public void Remove()
        {
            foreach (var expandable in Expandable)
            {
                Parent.Controls.Remove(expandable);
            }
            Expandable.Clear();
        }

        public void ChangeSelection(CustomSelection sel)
        {
            if (Parent != null && sel != this)
                Unselected(this, EventArgs.Empty);
           // TestCollapse(sel);
        }

        private void TestCollapse(CustomSelection sel)
        {
            if (Parent == null)
                return;
            if (sel == this)
            {
                m_Selected = true;
                m_Expanded = true;
            }
            else
            {
                if (!ContainsChild(sel))
                {
                    m_Expanded = false;
                    foreach (CustomSelection item in m_Expandable)
                        ((CustomSelectionPanel)Parent).RemoveSelectable(item);
                    if (m_Parent != null)
                        m_Parent.TestCollapse(sel);
                }
                m_Selected = false;
            }
        }

        private void SetupControl(Control c)
        {
            c.DoubleClick += ControlDoubleClicked;
            c.Click += new EventHandler(ControlClicked);
            foreach (Control control in c.Controls)
                SetupControl(control);
        }

        private bool ContainsChild(CustomSelection sel)
        {
            foreach (CustomSelection selection in m_Expandable)
            {
                if (sel == selection)
                    return true;
                if (selection.ContainsChild(sel))
                    return true;
            }
            return false;
        }
    }
}
