using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.Linq;

namespace EditorExpand.CreateAsset
{
    public class CreateAssetComponent : EndNameEditAction
    {

        [MenuItem("Assets/Create/ScriptTemplate/Component", false,81)]
        private static void CreateComponentScript()
        {
            // load text file at the path, and store the store the text
            var resourceFile = Path.Combine(
                Application.dataPath,
                "Editor/ScriptTemplateCreater/ScriptTemplates/Component.cs.txt");


            // use the C# icon provided by unity. 
            var csIcon =
                EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D;

            // create as an instance of ScriptableObject
            var endNameEditAction =
                ScriptableObject.CreateInstance<CreateAssetComponent>();

            // create Scripts from the selscted infomation in menu
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
                0,
                endNameEditAction,
                "NewComponentName.cs",
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

            var n = pathName.Split('/').Last();

            var dataPathName = pathName.Replace(n, name + "Component.cs");

            File.WriteAllText(dataPathName, text, encording);

            AssetDatabase.ImportAsset(dataPathName);
            var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(dataPathName);

            ProjectWindowUtil.ShowCreatedAsset(asset);
        }
    }
}