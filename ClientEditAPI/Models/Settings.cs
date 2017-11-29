using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using ClientEditAPI.Controllers;

class Settings
{
    private static string SettingsPath;

    public static XmlDocument XMLdoc;

    static public string SetSettingsPath()
    {
        int i;
        SettingsPath = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\";
        SettingsPath = SettingsPath +  "_Settings.xml";
        SettingsPath = SettingsPath.ToLower().Replace(".exe", "");
        string localPath = new Uri(SettingsPath).LocalPath;
        SettingsPath = localPath;
      return  Vars.SettingsPath = SettingsPath;
    }

    static public bool SaveSettings()
    {
        try
        {
            XMLdoc.Save(SettingsPath);
        }
        catch
        {

        }
        return true;
    }

    static public bool ReadSettings()
    {
        try
        {
            SetSettingsPath();
            if (File.Exists(SettingsPath) != true)
            {
                CreateSettingsFile();
            }

            try
            {
                XMLdoc = new XmlDocument();
                XMLdoc.Load(SettingsPath);
            }
            catch (Exception Ex)
            {
                CreateSettingsFile();
                XMLdoc = new XmlDocument();
                XMLdoc.Load(SettingsPath);
            }
        }
        catch(Exception ex)
        {
            ex.Message.ToString();
        }

        return true;
    }

    static public bool CreateSettingsFile()
    {
        string strXML;
        System.IO.StreamWriter SettingsFile;

        strXML = "<settings>" + "\r\n";
        strXML = strXML + @"<Clients>" + "\r\n";
        strXML = strXML + @"<Client FirstName= ""Joe Peter"" Surname= ""Bloggs"" IdentityType =""Identity Document"" IdentityNumber=""7202025074084"" DOB= ""1972/02/02"" />" + "\r\n";
        strXML = strXML + @"</Clients>" + "\r\n";
        strXML = strXML + @"</settings>" + "\r\n";

        SettingsFile = File.CreateText(SettingsPath);
        SettingsFile.WriteLine(strXML);
        SettingsFile.Close();
        return true;

    }
}
