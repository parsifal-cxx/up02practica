using System;

namespace LibraryAIS
{
    /// <summary>
    /// Вспомогательный класс для управления пагинацией
    /// </summary>
    public class PaginationHelper
    {
        /// <summary>
        /// Текущая страница (начинается с 1)
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Количество записей на одной странице
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Общее количество записей в выборке
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (PageSize == 0) return 0;
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }

        /// <summary>
        /// Смещение для SQL запроса (OFFSET)
        /// </summary>
        public int Offset
        {
            get { return (CurrentPage - 1) * PageSize; }
        }

        /// <summary>
        /// Есть ли предыдущая страница
        /// </summary>
        public bool HasPreviousPage
        {
            get { return CurrentPage > 1; }
        }

        /// <summary>
        /// Есть ли следующая страница
        /// </summary>
        public bool HasNextPage
        {
            get { return CurrentPage < TotalPages; }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pageSize">Размер страницы (по умолчанию 20)</param>
        public PaginationHelper(int pageSize = 20)
        {
            PageSize = pageSize;
            CurrentPage = 1;
            TotalRecords = 0;
        }

        /// <summary>
        /// Получить номер первой записи на текущей странице
        /// </summary>
        public int GetStartRecord()
        {
            if (TotalRecords == 0) return 0;
            return Offset + 1;
        }

        /// <summary>
        /// Получить номер последней записи на текущей странице
        /// </summary>
        public int GetEndRecord()
        {
            if (TotalRecords == 0) return 0;
            return Math.Min(Offset + PageSize, TotalRecords);
        }

        /// <summary>
        /// Переход на первую страницу
        /// </summary>
        public void GoToFirstPage()
        {
            CurrentPage = 1;
        }

        /// <summary>
        /// Переход на последнюю страницу
        /// </summary>
        public void GoToLastPage()
        {
            CurrentPage = TotalPages;
        }

        /// <summary>
        /// Переход на следующую страницу
        /// </summary>
        public bool GoToNextPage()
        {
            if (HasNextPage)
            {
                CurrentPage++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Переход на предыдущую страницу
        /// </summary>
        public bool GoToPreviousPage()
        {
            if (HasPreviousPage)
            {
                CurrentPage--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Переход на конкретную страницу
        /// </summary>
        public bool GoToPage(int pageNumber)
        {
            if (pageNumber >= 1 && pageNumber <= TotalPages)
            {
                CurrentPage = pageNumber;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Сброс на первую страницу (используется при изменении фильтров)
        /// </summary>
        public void Reset()
        {
            CurrentPage = 1;
        }

        /// <summary>
        /// Получить диапазон страниц для отображения кнопок
        /// </summary>
        /// <param name="maxButtons">Максимальное количество кнопок</param>
        /// <returns>Массив номеров страниц для отображения</returns>
        public int[] GetPageRange(int maxButtons = 7)
        {
            if (TotalPages <= maxButtons)
            {
                // Показываем все страницы
                int[] allPages = new int[TotalPages];
                for (int i = 0; i < TotalPages; i++)
                {
                    allPages[i] = i + 1;
                }
                return allPages;
            }

            // Показываем диапазон вокруг текущей страницы
            int halfButtons = maxButtons / 2;
            int startPage = Math.Max(1, CurrentPage - halfButtons);
            int endPage = Math.Min(TotalPages, CurrentPage + halfButtons);

            // Корректировка если подошли к краям
            if (CurrentPage <= halfButtons)
            {
                endPage = Math.Min(TotalPages, maxButtons);
            }
            else if (CurrentPage >= TotalPages - halfButtons)
            {
                startPage = Math.Max(1, TotalPages - maxButtons + 1);
            }

            int rangeSize = endPage - startPage + 1;
            int[] range = new int[rangeSize];
            for (int i = 0; i < rangeSize; i++)
            {
                range[i] = startPage + i;
            }

            return range;
        }
    }
}