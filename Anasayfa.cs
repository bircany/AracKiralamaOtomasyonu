using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rentacar
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            frmMusteriEkle frm = new frmMusteriEkle();
            frm.ShowDialog();
        }
        private void btnMusteriListele_Click(object sender, EventArgs e)
        {
            frmMusteriListele frm = new frmMusteriListele();
            frm.ShowDialog();
        }
        private void btnAracEkle_Click(object sender, EventArgs e)
        {
            frmAracEkle frm = new frmAracEkle();
            frm.ShowDialog();
        }
        private void btnAracListele_Click(object sender, EventArgs e)
        {
            frmAracListele frm = new frmAracListele();
            frm.ShowDialog();
        }
        private void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            frmPersonelEkle frm = new frmPersonelEkle();
            frm.ShowDialog();
        }
        private void btnPersonelListele_Click(object sender, EventArgs e)
        {
            frmPersonelListele frm = new frmPersonelListele();
            frm.ShowDialog();
        }
        private void btnSubeEkle_Click(object sender, EventArgs e)
        {
            frmSubeEkle frm = new frmSubeEkle();
            frm.ShowDialog();
        }
        private void btnSubeListele_Click(object sender, EventArgs e)
        {
            frmSubeListele frm = new frmSubeListele();
            frm.ShowDialog();
        }
        private void btnAracTipiEkle_Click(object sender, EventArgs e)
        {
            frmAracTipiEkle frm = new frmAracTipiEkle();
            frm.ShowDialog();
        }
        private void btnAracTipiListele_Click(object sender, EventArgs e)
        {
            FrmAracTipiListele frm = new FrmAracTipiListele();
            frm.ShowDialog();
        }
        private void btnMarkaEkle_Click(object sender, EventArgs e)
        {
            frmMarkaEkle frm = new frmMarkaEkle();
            frm.ShowDialog();
        }
        private void btnMarkaListele_Click(object sender, EventArgs e)
        {
            frmMarkaListele frm = new frmMarkaListele();
            frm.ShowDialog();
        }
        private void btnAracKiralama_Click(object sender, EventArgs e)
        {
            frmSozlesme frm = new frmSozlesme();
            frm.ShowDialog();
        }
        private void btnSigortaEkle_Click(object sender, EventArgs e)
        {
            frmSigortaEkle frm = new frmSigortaEkle();
            frm.ShowDialog();
        }
        private void btnSigortaListele_Click(object sender, EventArgs e)
        {
            frmSigortaListele frm = new frmSigortaListele();
            frm.ShowDialog();
        }
        private void btnServisEkle_Click(object sender, EventArgs e)
        {
            frmServisEkle frm = new frmServisEkle();
            frm.ShowDialog();
        }
        private void btnServisListele_Click(object sender, EventArgs e)
        {
            frmServisListele frm = new frmServisListele();
            frm.ShowDialog();
        }
        private void btnAracSigortasıEkle_Click(object sender, EventArgs e)
        {
            frmAracSigortaEkleme frm = new frmAracSigortaEkleme();
            frm.ShowDialog();
        }
        private void btnAracSigortasıListele_Click(object sender, EventArgs e)
        {
            frmAracSigortaListeleme frm = new frmAracSigortaListeleme();
            frm.ShowDialog();
        }
        private void btnAracServisiEkle_Click(object sender, EventArgs e)
        {
            frmAracServisEkleme frm = new frmAracServisEkleme();
            frm.ShowDialog();
        }
        private void btnAracServisiListele_Click(object sender, EventArgs e)
        {
            frmAracServisListeleme frm = new frmAracServisListeleme();
            frm.ShowDialog();
        }

        private void btnOdemelerListele_Click(object sender, EventArgs e)
        {
            frmOdemeler frm = new frmOdemeler();
            frm.ShowDialog();
        }
    }
}


