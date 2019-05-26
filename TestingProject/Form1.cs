using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Inventor;

namespace TestingProject
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// ThisApplication - Объект для определения активного состояния Инвентора
        /// </summary>
        private Inventor.Application ThisApplication = null;
        /// <summary>
        /// Словарь для хранения ссылок на документы деталей
        /// </summary>
        private Dictionary<string, PartDocument> PartDoc = new Dictionary<string, PartDocument>();
        /// <summary>
        /// Словарь для хранения ссылок на определения деталей
        /// </summary>
        private Dictionary<string, PartComponentDefinition> CompDef = new Dictionary<string, PartComponentDefinition>();
        /// <summary>
        /// Словарь для хранения ссылок на инструменты создания деталей
        /// </summary>
        private Dictionary<string, TransientGeometry> TransGeom = new Dictionary<string, TransientGeometry>();
        /// <summary>
        /// Словарь для хранения ссылок на транзакции редактирования
        /// </summary>
        private Dictionary<string, Transaction> Trans = new Dictionary<string, Transaction>();
        /// <summary>
        /// Словарь для хранения имен сохраненных документов деталей
        /// </summary>
        private Dictionary<string, string> FileName = new Dictionary<string, string>();

        private AssemblyDocument AssemblyDocName;


        private void CreateDoc(String str)
        {
            // Новый документ детали
            PartDoc[str] = (PartDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));
            // Новое определение
            CompDef[str] = PartDoc[str].ComponentDefinition;
            // Выбор инструментов
            TransGeom[str] = ThisApplication.TransientGeometry;
            // Создание транзакции
            Trans[str] = ThisApplication.TransactionManager.StartTransaction(ThisApplication.ActiveDocument, "Create Sample");
            // Имя файла
            FileName[str] = null;

            PartDoc[str].DisplayName = str;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonOpenInventor_Click(object sender, EventArgs e)
        {
            System.Activator.CreateInstance(Type.GetTypeFromProgID("Inventor.Application", true));
            ThisApplication = (Inventor.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Inventor.Application");
            ThisApplication.Visible = true;
        }

        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            string docName = "Первая деталь";
            CreateDoc(docName);

            // Создание эскиза на YX
            PlanarSketch Sketch = CompDef[docName].Sketches.Add(CompDef[docName].WorkPlanes[3]);    // 1 - YZ ; 2 - ZX; 3 - XY

            // Создание массивов Точек, Линий, Дуг, Окружностей
            List<SketchPoint>   points  = new List<SketchPoint>();   // Точки
            List<SketchLine>    lines   = new List<SketchLine>();    // Линии
            List<SketchArc>     arcs    = new List<SketchArc>();     // Дуги
            List<SketchCircle>  circles = new List<SketchCircle>();  // Окружности

            /////////////////////////////////////////////////////////// Выдавливание

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(0, 0), false));
            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(0, 5.0 / 10), false));
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[points.Count-2], points[points.Count - 1]));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(0, 10.0 / 10), false));
            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(0, 15.0 / 10), false));
            arcs.Add(Sketch.SketchArcs.AddByCenterStartEndPoint(points[points.Count - 2], points[points.Count - 3], points[points.Count - 1], true));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(0, 20.0 / 10), false));
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[points.Count - 2], points[points.Count - 1]));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(20.0 / 10, 20.0 / 10), false));
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[points.Count - 2], points[points.Count - 1]));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(20.0 / 10, 0), false));
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[points.Count - 2], points[points.Count - 1]));

            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[0], points[points.Count - 1]));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(10.0 / 10, 10.0 / 10), false));
            circles.Add(Sketch.SketchCircles.AddByCenterRadius(points[points.Count - 1], 4.0 / 10));

            Profile ProfileMain = (Profile)Sketch.Profiles.AddForSolid();

            ExtrudeFeature ExtrudeDef = CompDef[docName].Features.ExtrudeFeatures.AddByDistanceExtent(
                                        /*Эскиз*/ProfileMain,
                                        /*Длина в см*/10.0 / 10,
                                        /*Направление вдоль оси*/PartFeatureExtentDirectionEnum.kPositiveExtentDirection, 
                                        /*Операция*/PartFeatureOperationEnum.kJoinOperation,
                                        /*Эскиз*/ProfileMain);

            /////////////////////////////////////////////////////////// Вращение

            WorkPlane WorkinPlace = CompDef[docName].WorkPlanes.AddByPlaneAndOffset(
                                    CompDef[docName].WorkPlanes[2], 20.0 / 10);
            WorkinPlace.Visible = false;
            Sketch = CompDef[docName].Sketches.Add(WorkinPlace);

            SketchLine line_axis;
            points.Clear();
            lines.Clear();

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(-(10.0 - 5)/ 10, 0), false));
            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(-(10.0 - 5)/ 10, 10.0 / 10), false));
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[points.Count - 2], points[points.Count - 1]));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(-10.0 / 10, 10.0 / 10), false));
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[points.Count - 2], points[points.Count - 1]));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(-10.0 / 10, 0), false));
            line_axis = Sketch.SketchLines.AddByTwoPoints(points[points.Count - 2], points[points.Count - 1]);

            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[0], points[points.Count - 1]));

            Profile ProfileExternal = (Profile)Sketch.Profiles.AddForSolid();
            RevolveFeature revolvefeature = CompDef[docName].Features.RevolveFeatures.AddFull(
                                            ProfileExternal, 
                                            line_axis, 
                                            PartFeatureOperationEnum.kCutOperation);

            Trans[docName].End();

            /////////////////////////////////////////////////////////// Вторая деталь

            docName = "Вторая деталь";
            CreateDoc(docName);

            Sketch = CompDef[docName].Sketches.Add(CompDef[docName].WorkPlanes[3]);

            points.Clear();
            lines.Clear();

            ///////////////////////////////////////////////// Шестиугольник

            double R = 2 * (4.0 / 10) / Math.Sqrt(3), Angle;

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(
                                                    1.65 * Math.Cos(((double)210 / 180) * Math.PI),
                                                    1.65 * Math.Sin(((double)210 / 180) * Math.PI)), true));

            points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(points[0].Geometry.X + R, points[0].Geometry.Y), true));
            for (int i = 2; i < 7; i++)
            {
                Angle = ((((double)i - 2) * 60 + 120) / 180);
                points.Add(Sketch.SketchPoints.Add(TransGeom[docName].CreatePoint2d(
                                                        points[i - 1].Geometry.X + R * Math.Cos(Angle * Math.PI),
                                                        points[i - 1].Geometry.Y + R * Math.Sin(Angle * Math.PI)), true));

                lines.Add(Sketch.SketchLines.AddByTwoPoints(points[i - 1], points[i]));
            }
            lines.Add(Sketch.SketchLines.AddByTwoPoints(points[6], points[1]));

            ProfileMain = (Profile)Sketch.Profiles.AddForSolid();

            ExtrudeDef = CompDef[docName].Features.ExtrudeFeatures.AddByDistanceExtent(
                                            /*Эскиз*/ProfileMain,
                                            /*Длина*/10.0 / 10,
                                            /*Направление вдоль оси*/PartFeatureExtentDirectionEnum.kPositiveExtentDirection, 
                                            /*Операция*/PartFeatureOperationEnum.kJoinOperation,
                                            /*Эскиз*/ProfileMain);

            ///////////////////////////////////////////////// Фаска
            ChamferFeature Fillet;

            EdgeCollection Edges = ThisApplication.TransientObjects.CreateEdgeCollection();

            int k = 0;
            foreach (SurfaceBody SurfBody in PartDoc[docName].ComponentDefinition.SurfaceBodies)
            {
                foreach (Edge Edge in SurfBody.Edges)
                {
                    if (k == 2)
                        Edges.Add(Edge);
                    if (k == 5)
                        Edges.Add(Edge);
                    if (k == 8)
                        Edges.Add(Edge);
                    if (k == 11)
                        Edges.Add(Edge);
                    if (k == 14)
                        Edges.Add(Edge);
                    if (k == 17)
                        Edges.Add(Edge);
                    k++;
                }
            }

            Fillet = CompDef[docName].Features.ChamferFeatures.AddUsingDistance(Edges, 2.0/10);

            ///////////////////////////////////////////////// СОПРЯЖЕНИЕ
            FilletFeature Fillet1;
            Edges.Clear();

            int n = 0;
            foreach (SurfaceBody SurfBody in PartDoc[docName].ComponentDefinition.SurfaceBodies)
            {
                foreach (Edge Edge in SurfBody.Edges)
                {
                    if (n == 18)
                        Edges.Add(Edge);
                    if (n == 20)
                        Edges.Add(Edge);
                    if (n == 21)
                        Edges.Add(Edge);
                    if (n == 23)
                        Edges.Add(Edge);
                    if (n == 26)
                        Edges.Add(Edge);
                    if (n == 27)
                        Edges.Add(Edge);

                    n++;
                }
            }

            Fillet1 = CompDef[docName].Features.FilletFeatures.AddSimple(Edges, /*Радиус*/ 1.0 / 10);

            // Заврешение транзакции
            Trans[docName].End();
        }

        private void ButtonAssemble_Click(object sender, EventArgs e)
        {
            AssemblyDocument AssDoc = (AssemblyDocument)ThisApplication.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, ThisApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject));
            AssemblyComponentDefinition oAssCompDef = AssDoc.ComponentDefinition;
            AssDoc.DisplayName = "Сборка";
            TransientGeometry aTransGeom = ThisApplication.TransientGeometry;
            Matrix oPositionMatrix = aTransGeom.CreateMatrix();

            ComponentOccurrence IN_Ring;
            ComponentOccurrence Av_Ring;

            try
            {
                Savers("Первая деталь");
                IN_Ring = AssDoc.ComponentDefinition.Occurrences.Add(FileName["Первая деталь"], oPositionMatrix);

                Savers("Вторая деталь");
                Av_Ring = AssDoc.ComponentDefinition.Occurrences.Add(FileName["Вторая деталь"], oPositionMatrix);               

            }
            catch
            {
                return;
            }

            //Переменные для выбранных поверхностей
            Face oFace1, oFace2;
            //Вставка изменения программного кода

            oFace1 = IN_Ring.SurfaceBodies[1].Faces[10];
            oFace2 = Av_Ring.SurfaceBodies[1].Faces[12];
            //Конец вставки изменения программного кода
            //Переменные для сопряжений
            MateConstraint Поверхность;
            //Сопряжение  поверхностей

            Поверхность = oAssCompDef.Constraints.AddMateConstraint(oFace1, oFace2, 0, InferredTypeEnum.kNoInference, InferredTypeEnum.kNoInference);

            AssemblyDocName = AssDoc;
        }

        private void ButtonGetEdgeNumber_Click(object sender, EventArgs e)
        {
            PartDocument doc = (PartDocument)ThisApplication.ActiveDocument;
            int n = doc.SelectSet.Count;
            
            if (n > 0)
            {
                Object ob = doc.SelectSet[1];

                int edgeNumber = 0;
                int seeknumber = -1;
                foreach (SurfaceBody SurfBody in doc.ComponentDefinition.SurfaceBodies)
                {
                    foreach (Edge oEdge in SurfBody.Edges)
                    {
                        if (ob.Equals(oEdge))
                        {
                            seeknumber = edgeNumber;
                        }

                        edgeNumber++;
                    }
                }
                MessageBox.Show("" + seeknumber);
            }
        }

        private void ButtonGetFaceNumber_Click(object sender, EventArgs e)
        {
            PartDocument doc = (PartDocument)ThisApplication.ActiveDocument;
            int n = doc.SelectSet.Count;

            if (n > 0)
            {
                Object ob = doc.SelectSet[1];

                int edgeNumber = 0;
                int seeknumber = -1;
                foreach (SurfaceBody SurfBody in doc.ComponentDefinition.SurfaceBodies)
                {
                    foreach (Face oEdge in SurfBody.Faces)
                    {
                        if (ob.Equals(oEdge))
                        {
                            seeknumber = edgeNumber;
                        }

                        edgeNumber++;
                    }
                }
                MessageBox.Show("" + seeknumber);
            }
        }

        private int Savers(string Name)
        {

            saveFileDialog.Filter = "Inventor Part Document|*.ipt";
            saveFileDialog.Title = Name;
            saveFileDialog.FileName = PartDoc[Name].DisplayName;
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                    {
                        PartDoc[Name].SaveAs(saveFileDialog.FileName, false);
                        FileName[Name] = saveFileDialog.FileName;
                    }
                }
                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
