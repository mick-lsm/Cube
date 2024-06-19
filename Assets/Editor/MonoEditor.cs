using Cube.Components;
using Cube.Entity.AI;
using Cube.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace CustomEditor
{
    public sealed class MonoEditor : EditorWindow
    {
        //private List<SerializedObject> _monoBehaviours = new List<SerializedObject>();
        private GameObject _target;
        private SerializedObject health;
        private SerializedObject move;
        private SerializedObject jump;
        private SerializedObject shift;
        private SerializedObject interact;

        private SerializedObject weaponBehaviour;
        private WeaponBehaviour weaponBehaviourC;

        [MenuItem("Tools/MonoBehaviourEditor")]
        public static void ShowWindow()
        {
            GetWindow<MonoEditor>().Show();
        }

        private void OnGUI()
        {
            if (_target.IsUnityNull()) return;

            _target.name = EditorGUILayout.TextField("GameObject Name", _target.name);
            _target.transform.localPosition = EditorGUILayout.Vector3Field("GameObject Position", _target.transform.localPosition);
            _target.transform.localEulerAngles = EditorGUILayout.Vector3Field("GameObject Angle", _target.transform.localEulerAngles);
            _target.transform.localScale = EditorGUILayout.Vector3Field("GameObject Scale", _target.transform.localScale);

            if (health != null && health.targetObject != null) DisplayComponent(health);
            var existBehaviour = (move != null && jump != null && shift != null && interact != null) && (move.targetObject != null || jump.targetObject != null || shift.targetObject != null || interact.targetObject != null);
            if (existBehaviour) EditorGUILayout.LabelField("======Behaviours=====");
            if (move != null && move.targetObject != null) DisplayComponent(move);
            if (jump != null && jump.targetObject != null) DisplayComponent(jump);
            if (shift != null && shift.targetObject != null) DisplayComponent(shift);
            if (interact != null && interact.targetObject != null) DisplayComponent(interact);

            if(weaponBehaviour != null && weaponBehaviour.targetObject != null)
            {
                var des = weaponBehaviour.FindProperty("description");
                foreach(var v in des)
                {
                    if (v is not SerializedProperty prop) continue;
                    EditorGUILayout.PropertyField(prop);
                }

                var speedSlow = weaponBehaviour.FindProperty("speedScale");
                EditorGUILayout.PropertyField(speedSlow);
            }
            //foreach(var obj in _monoBehaviours)
            //{
            //    {
            //        var iteractor = obj.GetIterator();
            //        var temp = true;
            //        var dontCount = 1;
            //        while (iteractor.NextVisible(temp))
            //        {
            //            temp = false;
            //            if (dontCount != 0) dontCount--;
            //            else
            //            {
            //                EditorGUILayout.PropertyField(iteractor);
            //            }
            //        }
            //    }
            //    obj.ApplyModifiedProperties();
            //}
        }
        private void DisplayComponent(SerializedObject obj)
        {
            var iterator = obj.GetIterator();
            var temp = true;
            var dontCount = 1;

            while (iterator.NextVisible(temp))
            {
                temp = false;
                if (dontCount != 0) dontCount--;
                else
                {
                    EditorGUILayout.PropertyField(iterator);
                }
            }
            EditorGUILayout.Space();
        }
        private void SetComponents()
        {
            var healthC = _target.GetComponent<Health>();
            var jumpC = _target.GetComponent<Jumpable>();
            var shiftC = _target.GetComponent<ShiftDownDoable>();
            var interactC = _target.GetComponent<InteractionDoable>();
            var moveC = _target.GetComponent<Moveable>();
            weaponBehaviourC = _target.GetComponent<WeaponBehaviour>();

            if (healthC != null) health = new SerializedObject(healthC);
            if (jumpC != null) move = new SerializedObject(moveC);
            if (interactC != null) jump = new SerializedObject(jumpC);
            if (shiftC != null) shift = new SerializedObject(shiftC);
            if (interactC != null) interact = new SerializedObject(interactC);

            if (weaponBehaviourC != null) weaponBehaviour = new SerializedObject(weaponBehaviourC);
        }

        private void OnSelectionChange()
        {
            health = null;
            jump = null;
            interact = null;
            move = null;
            shift = null;
            weaponBehaviour = null;
            if (Selection.activeGameObject != null)
            {
                var g = Selection.activeGameObject;
                _target = g;
                SetComponents();

            }
        }
    }
}
