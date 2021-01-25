using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;

namespace MochimagroEditor.CreateAsset
{
    public class CreateAssetMVP : EndNameEditAction
    {
        [MenuItem("Assets/Create/ScriptTemplate/MVP", false, -1)]
        private static void CreateMonoBehaviour()
        {
            // unityで用意されているC#のアイコンを利用する
            var groupIcon =
                EditorGUIUtility.IconContent("TreeEditor.Duplicate").image as Texture2D;

            // ScriptableObjectのインスタンスとして作成する
            var endNameEditAction =
                ScriptableObject.CreateInstance<CreateAssetMVP>();

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                endNameEditAction,
                "NewMVPGroup",
                groupIcon,
                string.Empty);

        }

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            string[] fileType = new string[] { "Model", "View", "Presenter" };

            var groupName = Path.GetFileNameWithoutExtension(pathName);
            var pathes = pathName.Split('/');

            // ディレクトリの作成
            var directoryPath = pathName.Replace(pathes[pathes.Length - 1], groupName);
            Directory.CreateDirectory(directoryPath);

            foreach (var type in fileType)
            {
                var resourceModelFile = Path.Combine(
                    Application.dataPath,
                    "Editor/ScriptTemplateCreater/ScriptTemplates/" + type + ".cs.txt");
                CreateScript(directoryPath, resourceModelFile, type);
            }

            return;

        }

        private void CreateScript(string pathName, string resourceFile, string fileType)
        {
            var text = File.ReadAllText(resourceFile);
            var pathes = Application.dataPath.Split('/');

            var groupName = Path.GetFileNameWithoutExtension(pathName);
            var projectName = pathes[pathes.Length - 2];
            projectName = projectName.Replace(" ", "");

            text = text.Replace("#NAME#", groupName);
            text = text.Replace("#NAMETOLOWER#", char.ToLower(groupName[0]) + groupName.Substring(1));
            text = text.Replace("#PROJECTNAME#", projectName);
            text = text.Replace("#NOTRIM#", "\n");

            var encording = new UTF8Encoding(true, false);

            var filePath = pathName + "/" + groupName + fileType + ".cs";

            File.WriteAllText(filePath, text, encording);

            AssetDatabase.ImportAsset(filePath);
            var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);

            ProjectWindowUtil.ShowCreatedAsset(asset);


        }
    }

}