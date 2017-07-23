using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TensorFlow;

public class TfTest : MonoBehaviour {

	string displayText;

	// Use this for initialization
	void Start () {
		displayText = "Trying tensorflow...";

		try {
			TryTensorflow();
		} catch (System.Exception e) {
			displayText = $"It broke: {e}";
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetComponentInChildren<Text>().text = displayText;
	}

	void TryTensorflow () {
		using (var graph = new TFGraph())
		{
			var x = graph.Const(3.0);
			var y = graph.Const(2.0);
			var z = graph.Const(5.0);

			var h = graph.Add(x, y);
			var g = graph.Mul(h, z);

			using (var sess = new TFSession(graph))
			{
				var runner = sess.GetRunner();
				TFTensor result = runner.Run(g);
				displayText = $"It worked!\n(x+y)*z\n= {result}";
			}
		}
	}
}
