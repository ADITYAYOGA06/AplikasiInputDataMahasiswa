using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInputDataMahasiswa
{
    public partial class Form1 : Form
    {
        public class Mahasiswa
        {
            public string Nim;
            public string Nama;
            public string Kelas;
            public int Nilai;
            public string nilaiHuruf;
        }
        
        //deklarasikan objek collection
        private List<Mahasiswa> list = new List<Mahasiswa>();

        //constructor
        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();
        }
        //atur kolom listview
        private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;

            lvwMahasiswa.Columns.Add("No ", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nim ", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama ", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas ", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai ", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai Huruf ", 91, HorizontalAlignment.Center);
        }
        private void ResetForm()
        {
            txtNim.Clear();
            txtNama.Clear();
            txtKelas.Clear();
            txtNilai.Text = "0";
            txtNim.Focus();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";
            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }
        private void txtNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            //membuat objek mahasiswa
            Mahasiswa mhs = new Mahasiswa();

            //set nilai masing-masing propertynya
            //berdasarkan inputan yang ada di form
            mhs.Nim = txtNim.Text;
            mhs.Nama = txtNama.Text;
            mhs.Kelas = txtKelas.Text;
            mhs.Nilai = int.Parse(txtNilai.Text);
            
            if(mhs.Nilai>0 && mhs.Nilai<21)
            {
                mhs.nilaiHuruf = "E";
            }else if(mhs.Nilai > 20 && mhs.Nilai < 41)
            {
                mhs.nilaiHuruf = "D";
            }else if(mhs.Nilai > 40 && mhs.Nilai < 61)
            {
                mhs.nilaiHuruf = "C";
            }else if(mhs.Nilai > 60 && mhs.Nilai < 81)
            {
                mhs.nilaiHuruf = "B";
            }else if(mhs.Nilai > 80 && mhs.Nilai < 101)
            {
                mhs.nilaiHuruf = "A";
            }
            else
            {
                mhs.nilaiHuruf = "Undefined";
            }

            //tambahkan objek mahasiswa ke dalam collection
            list.Add(mhs);

            var msg = "Data mahasiswa berhasil disimpan.";

            //tampilkan dialog informasi
            MessageBox.Show(msg, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //reset form input
            ResetForm();
        }
        private void TampilkanData()
        {
            //kosongkan data listview
            lvwMahasiswa.Items.Clear();

            //lakukan perulangan untuk menampilkan data mahasiswa ke listview
            foreach(var mhs in list)
            {
                var noUrut = lvwMahasiswa.Items.Count + 1;

                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Nim);
                item.SubItems.Add(mhs.Nama);
                item.SubItems.Add(mhs.Kelas);
                item.SubItems.Add(mhs.Nilai.ToString());
                item.SubItems.Add(mhs.nilaiHuruf);

                lvwMahasiswa.Items.Add(item);
            }

        }

        private void btnTampilkanData_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            //cek apakah data mahasiswa sudah dipilih
            if(lvwMahasiswa.SelectedItems.Count > 0)
            {
                //tampilkan konfirmasi
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if(konfirmasi == DialogResult.Yes)
                {
                    //ambil index list yang dipilih 
                    var index = lvwMahasiswa.SelectedIndices[0];

                    //hapus objek mahasiswa dari list
                    list.RemoveAt(index);

                    //refresh tampilan listview
                    TampilkanData();
                }
            }
            else  // data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih!!!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }


}
