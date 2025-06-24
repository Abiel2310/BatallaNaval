using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatallaNaval.Controladores;
using BatallaNaval.Modelos;

namespace BatallaNaval.Forms
{
    public partial class Practica : Form
    {
        private List<Celda> celdasPractica = [];
        private List<Panel> panelesPractica = [];
        private List<Barco> barcosEnemigoPractica = [];
        private List<Celda> celdasAtacadasPractica = [];

        public Practica()
        {
            InitializeComponent();
            InicializarPractica();
        }
        private void InicializarPractica()
        {
            gridPractica.Controls.Clear();
            celdasPractica.Clear();
            panelesPractica.Clear();
            barcosEnemigoPractica.Clear();
            celdasAtacadasPractica.Clear();

            // Crear barcos enemigos
            Barco portaAviones = new()
            {
                Id = 1,
                CantidadCeldas = 5,
                NombreBarco = "Porta Aviones",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoGrande = new()
            {
                Id = 2,
                CantidadCeldas = 4,
                NombreBarco = "Destructor",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoUnPocoMasChico = new()
            {
                Id = 3,
                CantidadCeldas = 3,
                NombreBarco = "Cruiser",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoChico = new()
            {
                Id = 4,
                CantidadCeldas = 2,
                NombreBarco = "Patrulla",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoChiquito = new()
            {
                Id = 5,
                CantidadCeldas = 1,
                NombreBarco = "nave",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };

            barcosEnemigoPractica = [portaAviones, BarcoGrande, BarcoUnPocoMasChico, BarcoChico, BarcoChiquito];

            int contador = 1;
            for (int i = 0; i < gridPractica.RowCount; i++)
            {
                for (int j = 0; j < gridPractica.ColumnCount; j++)
                {
                    Celda celda = new()
                    {
                        Id = contador,
                        ContieneBarco = false,
                        Atacada = false,
                        Fila = i,
                        Columna = j
                    };
                    celdasPractica.Add(celda);

                    Panel p = new()
                    {
                        Dock = DockStyle.Fill,
                        Tag = celda.Id,
                        Margin = new Padding(0),
                    };
                    panelesPractica.Add(p);

                    p.Click += (s, args) => ClickCeldaPractica(s, args, celda, p);

                    gridPractica.Controls.Add(p);
                    contador++;
                }
            }

            // Posicionar barcos enemigos
            PosicionarBarcosEnemigosPractica();
        }

        // Posiciona los barcos enemigos en el grid de práctica
        private void PosicionarBarcosEnemigosPractica()
        {
            var random = new Random();
            int filas = gridPractica.RowCount;
            int columnas = gridPractica.ColumnCount;

            foreach (var barco in barcosEnemigoPractica)
            {
                bool colocado = false;
                int intentos = 0;
                while (!colocado && intentos < 100)
                {
                    intentos++;
                    // 0 = horizontal, 1 = vertical
                    bool horizontal = random.Next(2) == 0;
                    int maxFila = horizontal ? filas : filas - barco.CantidadCeldas + 1;
                    int maxCol = horizontal ? columnas - barco.CantidadCeldas + 1 : columnas;

                    int fila = random.Next(0, maxFila);
                    int col = random.Next(0, maxCol);

                    // Verificar si hay espacio libre
                    bool espacioLibre = true;
                    List<Celda> posiblesCeldas = new();

                    for (int k = 0; k < barco.CantidadCeldas; k++)
                    {
                        int f = horizontal ? fila : fila + k;
                        int c = horizontal ? col + k : col;
                        var celda = celdasPractica.FirstOrDefault(x => x.Fila == f && x.Columna == c);
                        if (celda == null || celda.ContieneBarco)
                        {
                            espacioLibre = false;
                            break;
                        }
                        posiblesCeldas.Add(celda);
                    }

                    if (espacioLibre)
                    {
                        barco.CeldasPosicion = new List<Celda>();
                        foreach (var celda in posiblesCeldas)
                        {
                            celda.ContieneBarco = true;
                            celda.BarcoId = barco.Id;
                            barco.CeldasPosicion.Add(celda);
                        }
                        barco.EnPosicion = true;
                        colocado = true;
                    }
                }

                if (!colocado)
                {
                    MessageBox.Show($"No se pudo colocar el barco {barco.NombreBarco}. Intenta reiniciar la práctica.");
                }
            }
        }


        // Evento de click para atacar celdas y hundir barcos
        private void ClickCeldaPractica(object sender, EventArgs e, Celda celda, Panel p)
        {
            if (!celda.Atacada)
            {
                celda.Atacada = true;
                if (celda.ContieneBarco)
                {
                    p.BackColor = Color.Red;
                    celdasAtacadasPractica.Add(celda);

                    // Buscar el barco atacado
                    var barcoAtacado = barcosEnemigoPractica.FirstOrDefault(b => b.Id == celda.BarcoId);
                    if (barcoAtacado != null && barcoAtacado.CeldasPosicion.All(c => c.Atacada))
                    {
                        barcoAtacado.Hundido = true;
                        MessageBox.Show($"¡Hundiste el/la {barcoAtacado.NombreBarco}!");
                    }
                }
                else
                {
                    p.BackColor = Color.White;
                }

                p.Cursor = Cursors.Default;

                // Verificar si todos los barcos están hundidos
                if (barcosEnemigoPractica.All(b => b.Hundido))
                {
                    //MessageBox.Show("¡Has encontrado y hundido todos los barcos!");
                    // Cierrar la práctica o reiniciar
                    DialogResult result = MessageBox.Show("¡Has encontrado y hundido todos los barcos! ¿Deseas reiniciar la práctica?", "Práctica finalizada", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        InicializarPractica();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }
    }
}
