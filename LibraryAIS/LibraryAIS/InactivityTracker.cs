using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LibraryAIS
{
    public class InactivityTracker : IMessageFilter
    {
        private Timer inactivityTimer;
        private int timeoutSeconds;
        private bool isEnabled;

   
        public event EventHandler InactivityDetected;

        public InactivityTracker(int timeoutSeconds)
        {
            this.timeoutSeconds = timeoutSeconds;
            this.isEnabled = false;

            // Инициализация таймера
            inactivityTimer = new Timer();
            inactivityTimer.Interval = timeoutSeconds * 1000; // Переводим в миллисекунды
            inactivityTimer.Tick += InactivityTimer_Tick;
        }

        public void Start()
        {
            if (!isEnabled)
            {
                isEnabled = true;

                // Добавляем фильтр сообщений для перехвата событий
                Application.AddMessageFilter(this);

                // Запускаем таймер
                inactivityTimer.Start();
            }
        }

       
        public void Stop()
        {
            if (isEnabled)
            {
                isEnabled = false;

                // Останавливаем таймер
                inactivityTimer.Stop();

                // Удаляем фильтр сообщений
                Application.RemoveMessageFilter(this);
            }
        }

        public void ResetTimer()
        {
            if (isEnabled)
            {
                inactivityTimer.Stop();
                inactivityTimer.Start();
            }
        }


        public void UpdateTimeout(int newTimeoutSeconds)
        {
            bool wasRunning = inactivityTimer.Enabled;

            inactivityTimer.Stop();
            this.timeoutSeconds = newTimeoutSeconds;
            inactivityTimer.Interval = newTimeoutSeconds * 1000;

            if (wasRunning)
            {
                inactivityTimer.Start();
            }
        }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            // Останавливаем таймер
            inactivityTimer.Stop();

            // Вызываем событие блокировки
            OnInactivityDetected();
        }

        protected virtual void OnInactivityDetected()
        {
            InactivityDetected?.Invoke(this, EventArgs.Empty);
        }
        public bool PreFilterMessage(ref Message m)
        {
            // Константы сообщений Windows
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int WM_CHAR = 0x0102;
            const int WM_SYSKEYDOWN = 0x0104;
            const int WM_SYSKEYUP = 0x0105;
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP = 0x0202;
            const int WM_RBUTTONDOWN = 0x0204;
            const int WM_RBUTTONUP = 0x0205;
            const int WM_MBUTTONDOWN = 0x0207;
            const int WM_MBUTTONUP = 0x0208;
            const int WM_MOUSEWHEEL = 0x020A;
            const int WM_MOUSEHWHEEL = 0x020E;

            // Проверяем, является ли сообщение событием клавиатуры или мыши
            bool isActivity = false;

            switch (m.Msg)
            {
                // События клавиатуры
                case WM_KEYDOWN:
                case WM_KEYUP:
                case WM_CHAR:
                case WM_SYSKEYDOWN:
                case WM_SYSKEYUP:
                    isActivity = true;
                    break;

                // События мыши
                case WM_MOUSEMOVE:
                case WM_LBUTTONDOWN:
                case WM_LBUTTONUP:
                case WM_RBUTTONDOWN:
                case WM_RBUTTONUP:
                case WM_MBUTTONDOWN:
                case WM_MBUTTONUP:
                case WM_MOUSEWHEEL:
                case WM_MOUSEHWHEEL:
                    isActivity = true;
                    break;
            }

            // Если обнаружена активность - сбрасываем таймер
            if (isActivity && isEnabled)
            {
                ResetTimer();
            }

            return false;
        }

        public int GetRemainingSeconds()
        {
            if (!isEnabled || !inactivityTimer.Enabled)
                return timeoutSeconds;
            return timeoutSeconds;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
        }

        public void Dispose()
        {
            Stop();

            if (inactivityTimer != null)
            {
                inactivityTimer.Dispose();
                inactivityTimer = null;
            }
        }
    }
}