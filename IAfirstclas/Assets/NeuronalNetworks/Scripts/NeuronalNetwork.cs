using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronalNetwork : MonoBehaviour
{

    public struct Neuron
    {
        public int _inputs;
        public List<float> _weights;
        public Neuron(int inputs)
        {
            _inputs = inputs + 1;
            _weights = new List<float>();
            for (int i = 0; i < _inputs; i++)
            {
                _weights[i] = Random.Range(-1f, 1f);
            }
        }
    }
    public struct NeuronLayer
    {
        public int _neuronsCount;
        public List<Neuron> _neurons;
        public NeuronLayer(int neuronsCount, int inputs)
        {
            _neuronsCount = neuronsCount;
            _neurons = new List<Neuron>();
            for (int i = 0; i < _neuronsCount; i++)
            {
                _neurons[i] = new Neuron(inputs);
            }
        }
    }

    public int _inputs;
    public int _outputs;
    public int _hiddenLayers;
    public int _neuronsPerLayer;
    List<NeuronLayer> _layers;
    public float _bias;
    public float _linearGrade;

    public void CreateNet()
    {
        //create the layers of the network
        if (_hiddenLayers > 0)
        {
            _layers.Add(new NeuronLayer(_neuronsPerLayer, _inputs));
            for (int i = 0; i < _hiddenLayers - 1; ++i)
            {
                _layers.Add(new NeuronLayer(_neuronsPerLayer, _neuronsPerLayer));
            }
            _layers.Add(new NeuronLayer(_outputs, _neuronsPerLayer));
        }
        else
        {
            _layers.Add(new NeuronLayer(_outputs, _inputs));
        }
    }

    public List<float> UpdateNN(ref List<float> inputs)
    {
        //stores the resultant outputs from each layer
        List<float> outputs = new List<float>();
        int cWeight = 0;
        //first check that we have the correct amount of inputs
        if (inputs.Count != _inputs)
        {
            //just return an empty vector if incorrect.
            return outputs;
        }
        //For each layer...
        for (int i = 0; i < _hiddenLayers + 1; ++i)
        {
            if (i > 0)
            {
                inputs = outputs;
            }
            outputs.Clear();
            cWeight = 0;
            //for each neuron sum the inputs * corresponding weights. Throw
            //the total at the sigmoid function to get the output.
            for (int j = 0; j < _layers[i]._neuronsCount; ++j)
            {
                float activation = 0;
                int NumInputs = _layers[i]._neurons[j]._inputs;
                //for each weight
                for (int k = 0; k < NumInputs - 1; ++k)
                {
                    //sum the weights x inputs
                    activation += _layers[i]._neurons[j]._weights[k] * inputs[cWeight++];
                }

                activation += _layers[i]._neurons[j]._weights[NumInputs - 1] * _bias;
                //we can store the outputs from each layer as we generate them.
                //The combined activation is first filtered through the sigmoid
                //function
                outputs.Add(Sigmoid(activation));
             
                cWeight = 0;
            }
        }
        return outputs;
    }

    float Sigmoid(float activation)
    {
        float sigmoid = 0;
        sigmoid = (1 / (1 + Mathf.Exp(-activation / _linearGrade)));
        return sigmoid;
    }
}
