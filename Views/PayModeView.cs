namespace Supermarket_mvp.Views
{
    public partial class PayModeView : Form, IPayModeView
    {
        private bool isEdit;
        private bool isSuccessful;
        private string message;

        public PayModeView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();

            tabControl1.TabPages.Remove(tabPagePayModeDetail);
            BtnClose.Click += delegate { this.Close(); };
        }

        public string PayModeId { get { return TxtPayModeId.Text; } set { TxtPayModeId.Text = value; } }

        public string PayModeName { get { return TxtPayModeName.Text; } set { TxtPayModeName.Text = value; } }
        public string PayModeObservation { get { return TxtPayModeObservation.Text; } set { TxtPayModeObservation.Text = value; } }
        public string SerchValues { get { return TxtSearch.Text; } set { TxtSearch.Text = value; } }
        public bool IsEdit { get { return isEdit; } set { isEdit = value; } }
        public bool IsSuccessful { get { return isSuccessful; } set { isSuccessful = value; } }
        public string Message { get { return message; } set { message = value; } }



        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;


        public void SetPayModeListBildingSoursce(BindingSource payModeList)
        {
            DgPayMode.DataSource = payModeList;
        }

        private static PayModeView instance;

        public static PayModeView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PayModeView();
                //instance.MdiParent = parentContainer;

                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Maximized)
                {
                    instance.WindowState = FormWindowState.Normal;
                }
                instance.BringToFront();
            }
            return instance;
        }


        public string PaymodeId
        {
            get { return TxtPayModeId.Text; }
            set { TxtPayModeId.Text = value; }
        }
        public string PaymodeName
        {
            get { return TxtPayModeName.Text; }
            set { TxtPayModeName.Text = value; }

        }
        public string PaymdeObservation
        {
            get { return TxtPayModeObservation.Text; }
            set { TxtPayModeObservation.Text = value; }
        }
        public string SearchValues
        {
            get { return TxtSearch.Text; }
            set { TxtSearch.Text = value; }
        }

        public bool IsEditt
        {
            get { return isEdit; }
            set { isEdit = value; }
        }

        public bool IsSuccessfull
        {
            get { return isSuccessful; }
            set { isSuccessful = value; }
        }

        public string Messagee
        {
            get { return message; }
            set { message = value; }
        }


        private void AssociateAndRaiseViewEvents()
        {
            BtnSearch.Click += delegate { SaveEvent.Invoke(this, EventArgs.Empty); };
            TxtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent.Invoke(this, EventArgs.Empty);
                }
            };
        }


    }
}