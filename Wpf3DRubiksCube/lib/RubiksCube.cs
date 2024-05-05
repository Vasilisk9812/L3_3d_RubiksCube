using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;


namespace Wpf3DRubiksCube.lib
{
    internal class RubiksCube : ModelVisual3D
    {
        private bool _isCanAnimation = true;
        private readonly double _animationDuration = 0.2;
        private readonly double _size = 0.5;
        private readonly double _gap = 0.03;

        public RubiksCube()
        {
            Create(_size, _gap);
        }


        public bool IsCanAnimation => _isCanAnimation;


        #region  === Вращение вокруг оси Х ===

        public void RotateLeftX(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetX < -0.1;
            RotateX(clockwise, select);
        }

        public void RotateMiddleX(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetX > -0.1 && item.Transform.Value.OffsetX < 0.1;
            RotateX(clockwise, select);
        }

        public void RotateRightX(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetX > 0.1;
            RotateX(clockwise, select);
        }

        private void RotateX(bool clockwise, Func<Cube3D, bool> select)
        {
            Rotate(clockwise, new Vector3D(1, 0, 0), select);
        }

        #endregion

        #region === Вращение вокруг оси Y === 

        public void RotateTopY(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetY > 0.1;
            RotateY(clockwise, select);
        }

        public void RotateMiddleY(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetY > -0.1 && item.Transform.Value.OffsetY < 0.1;
            RotateY(clockwise, select);
        }

        public void RotateBottomY(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetY < -0.1;
            RotateY(clockwise, select);
        }

        private void RotateY(bool clockwise, Func<Cube3D, bool> select)
        {
            Rotate(clockwise, new Vector3D(0, 1, 0), select);
        }

        #endregion

        #region === Вращение вокруг оси Z ===

        public void RotateFrontZ(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetZ > 0.1;
            RotateZ(clockwise, select);
        }

        public void RotateMiddleZ(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetZ > -0.1 && item.Transform.Value.OffsetZ < 0.1;
            RotateZ(clockwise, select);
        }

        public void RotateBackZ(bool clockwise = false)
        {
            static bool select(Cube3D item) => item.Transform.Value.OffsetZ < -0.1;
            RotateZ(clockwise, select);
        }

        private void RotateZ(bool clockwise, Func<Cube3D, bool> select)
        {
            Rotate(clockwise, new Vector3D(0, 0, 1), select);
        }

        #endregion


        #region Закрытые методы

        /// <summary>
        /// Создание кубика сегментами из 9 кубиков сверху вниз по оси Y.
        /// </summary>
        /// <param name="size">размер кубиков и он же задаёт 
        /// смещение кубиков относительно соответствующих осей</param>
        /// <param name="gap">зазор между кубиками</param>
        private void Create(double size, double gap)
        {
            Segment(size, size + gap, gap);
            Segment(size, 0, gap);
            Segment(size, -(size + gap), gap);

            ColorFill();
        }

        private void Segment(double size, double y, double gap)
        {
            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = -(size + gap),
                    OffsetY = y,
                    OffsetZ = size + gap
                },
                Size = size
            });


            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = 0,
                    OffsetY = y,
                    OffsetZ = size + gap
                },
                Size = size
            });

            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = size + gap,
                    OffsetY = y,
                    OffsetZ = size + gap
                },
                Size = size
            });


            //
            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = -(size + gap),
                    OffsetY = y,
                    OffsetZ = 0
                },
                Size = size
            });

            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = 0,
                    OffsetY = y,
                    OffsetZ = size + gap
                },
                Size = size
            });

            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = 0,
                    OffsetY = y,
                    OffsetZ = 0
                },
                Size = size
            });

            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = size + gap,
                    OffsetY = y,
                    OffsetZ = 0
                },
                Size = size
            });


            //
            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = -(size + gap),
                    OffsetY = y,
                    OffsetZ = -(size + gap)
                },
                Size = size
            });

            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = 0,
                    OffsetY = y,
                    OffsetZ = -(size + gap)
                },
                Size = size
            });

            Children.Add(new Cube3D()
            {
                Transform = new TranslateTransform3D
                {
                    OffsetX = size + gap,
                    OffsetY = y,
                    OffsetZ = -(size + gap)
                },
                Size = size
            });
        }

        private void ColorFill()
        {
            foreach (Cube3D item in Children)
            {
                // Закраска передней стороны
                if (item.Transform.Value.OffsetZ > 0.1) item.Front = Brushes.Red;

                // Закраска задней стороны
                if (item.Transform.Value.OffsetZ < -0.1) item.Back = Brushes.Orange;

                // Левой
                if (item.Transform.Value.OffsetX < -0.1) item.Left = Brushes.Green;

                // Правой
                if (item.Transform.Value.OffsetX > 0.1) item.Right = Brushes.Blue;

                // Верхней
                if (item.Transform.Value.OffsetY > 0.1) item.Top = Brushes.White;

                // Нижней
                if (item.Transform.Value.OffsetY < -0.1) item.Bottom = Brushes.Yellow;
            }
        }

        private void Rotate(bool clockwise, Vector3D axisRotation, Func<Cube3D, bool> selectSegment)
        {
            if (_isCanAnimation == false) return;
            _isCanAnimation = false;

            RotateTransform3D rotate = new(new AxisAngleRotation3D(axisRotation, 0));

            foreach (Cube3D item in Children)
            {
                if (selectSegment(item) == true)
                {
                    item.Transform = new Transform3DGroup()
                    {
                        Children = { item.Transform, rotate }
                    };
                }
            }

            DoubleAnimation rotateAnimation = new()
            {
                Duration = TimeSpan.FromSeconds(_animationDuration)
            };

            rotateAnimation.By = clockwise == false ? 90 : -90;

            rotateAnimation.Completed += RotateAnimation_Completed;
            rotate.Rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotateAnimation);

            void RotateAnimation_Completed(object? sender, EventArgs e)
            {
                _isCanAnimation = true;
            }
        }

        #endregion

    }
}
