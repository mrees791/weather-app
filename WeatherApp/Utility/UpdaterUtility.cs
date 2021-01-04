using AutoUpdaterDotNET;
using System;
using System.Net;

namespace WeatherApp.Utility
{
    public class UpdaterUtility
    {
        /// <summary>
        /// Checks for updates using a guest account.
        /// </summary>
        public static void CheckForUpdates()
        {
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            string updateXmlUrl = @"ftp://192.168.0.71/weather_app/update.xml";
            string guestUsername = "guest";
            string guestPassword = "i44pie";
            var ftpCredentials = new NetworkCredential(guestUsername, guestPassword);

            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
            AutoUpdater.Start(updateXmlUrl, ftpCredentials);
        }

        private static void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    System.Windows.Forms.DialogResult dialogResult;
                    if (args.Mandatory.Value)
                    {
                        dialogResult =
                            System.Windows.Forms.MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. This is required update. Press Ok to begin updating the application.", @"Update Available",
                                System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Information);
                    }
                    else
                    {
                        dialogResult =
                            System.Windows.Forms.MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {
                                        args.InstalledVersion
                                    }. Do you want to update the application now?", @"Update Available",
                                System.Windows.Forms.MessageBoxButtons.YesNo,
                                System.Windows.Forms.MessageBoxIcon.Information);
                    }

                    // Uncomment the following line if you want to show standard update dialog instead.
                    // AutoUpdater.ShowUpdateForm(args);

                    if (dialogResult.Equals(System.Windows.Forms.DialogResult.Yes) || dialogResult.Equals(System.Windows.Forms.DialogResult.OK))
                    {
                        try
                        {
                            if (AutoUpdater.DownloadUpdate(args))
                            {
                                System.Windows.Application.Current.Shutdown();
                                //Application.Exit();
                            }
                        }
                        catch (Exception exception)
                        {
                            System.Windows.Forms.MessageBox.Show(exception.Message, exception.GetType().ToString(), System.Windows.Forms.MessageBoxButtons.OK,
                                System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(@"You are running the latest version of this application.", @"No update available",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(
                        @"There is a problem reaching update server please check your internet connection and try again later.",
                        @"Update check failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
