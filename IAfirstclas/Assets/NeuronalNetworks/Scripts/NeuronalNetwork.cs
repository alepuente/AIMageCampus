using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronalNetwork
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
                _weights.Add(Random.Range(-1f, 1f));
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
                _neurons.Add(new Neuron(inputs));
            }
        }
    }

    public int _inputs;
    public int _outputs;
    public int _hiddenLayers;
    public int _neuronsPerLayer;
    private List<NeuronLayer> _layers;
    public float _linearGrade;
    public float _bias;

    public NeuronalNetwork(int inputs, int outputs, int hiddenLayers, int neuronsPerLayer, float linearGrade, float bias)
    {
        _inputs = inputs;
        _outputs = outputs;
        _hiddenLayers = hiddenLayers;
        _neuronsPerLayer = neuronsPerLayer;
        _linearGrade = linearGrade;
        _bias = bias;
    }

    public void CreateNet()
    {
        _layers = new List<NeuronLayer>();
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

    public List<float> UpdateNN(List<float> inputs)
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
                inputs = new List<float>(outputs);
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

    public int TotalWeights()
    {
        int totalWeights = 0;
        foreach (NeuronLayer item in _layers)
        {
            totalWeights += (item._neuronsCount * _inputs) + item._neuronsCount;
        }
        return 0;
    }

    public NNChromosome GetWeights()
    {
        NNChromosome _weights = new NNChromosome();
        foreach (NeuronLayer layer in _layers)
        {
            foreach (Neuron neuron in layer._neurons)
            {
                foreach (float weight in neuron._weights)
                {
                    _weights._chromosome.Add(new NNChromosome.Gen(weight));
                }
            }
        }
        return _weights;
    }

    public void SetWeights(NNChromosome newWeights)
    {
        int aux = 0;
        for (int i = 0; i < _layers.Count; i++)
        {
            for (int f = 0; f < _layers[i]._neuronsCount; f++)
            {
                for (int g = 0; g < _layers[i]._neurons[f]._weights.Count; g++)
                {
                    _layers[i]._neurons[f]._weights[g] = newWeights._chromosome[aux]._weight;
                    aux++;
                }
            }
        }
    }
}
