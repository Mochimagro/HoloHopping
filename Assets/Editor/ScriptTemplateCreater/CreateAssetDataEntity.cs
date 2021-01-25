using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

namespace MochimagroEditor.CreateAsset
{
    public class CreateAssetDataEntity : EndNameEditAction
    {
        [MenuItem("Assets/Create/ScriptTemplate/DataAndEntity", false, -1)]
        private static void CreateMonoBehaviour()
        {
            var resourceFile = Path.Combine(
                Application.dataPath,
                "Editor/ScriptTemplateCreater/ScriptTemplates/Data.cs.txt");


            // unityで用意されているC#のアイコンを利用する
            var csIcon =
                EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

            // ScriptableObjectのインスタンスとして作成する
            var endNameEditAction =
                ScriptableObject.CreateInstance<CreateAssetDataEntity>();

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                endNameEditAction,
                "NewData.cs",
                csIcon,
                resourceFile);

        }

        public override void Action(int instanceId, string pathName, string resourceFile)
        {

            var text = File.ReadAllText(resourceFile);
            var pathes = Application.dataPath.Split('/');

            var name = Path.GetFileNameWithoutExtension(pathName);
            var scriptName = name.Replace(" ", "");
            var projectName = pathes[pathes.Length - 2];
            projectName = projectName.Replace(" ", "");
            var directryName = Path.GetDirectoryName(pathName).
                                Replace("Assets", "").
                                Replace("/Scripts", "").
                                Replace("/", ".");

            text = text.Replace("#NAME#", name);
            text = text.Replace("#SCRIPTNAME#", scriptName);
            text = text.Replace("#PROJECTNAME#", projectName);
            text = text.Replace("#DIRECTORYNAME#", directryName);
            text = text.Replace("#NOTRIM#", "\n");

            var encording = new UTF8Encoding(true, false);

            File.WriteAllText(pathName, text, encording);

            AssetDatabase.ImportAsset(pathName);
            var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(pathName);

            ProjectWindowUtil.ShowCreatedAsset(asset);
        }
    }
}