namespace Assignment9
{
    public class AppLogger
    {
        private static AppLogger logger = null;

        private AppLogger()
        {
        }

        public static AppLogger GetLogger()
        {
            if (logger == null)
            {
                logger = new AppLogger();
            }

            return logger;
        }
    }
}