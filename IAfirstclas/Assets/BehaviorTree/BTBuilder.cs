using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBuilder {
	public Sequencer<BlackBoard> BuildTree(BlackBoard blackboard)
    {
        Sequencer<BlackBoard> rootNode = new Sequencer<BlackBoard>(blackboard);

        Iddle iddleNode = new Iddle(blackboard);
        rootNode.AddChild(iddleNode);

        Sequencer<BlackBoard> mining = new Sequencer<BlackBoard>(blackboard);
        rootNode.AddChild(mining);
        GoToMine goMineNode = new GoToMine(blackboard);
        Mine mineNode = new Mine(blackboard);
        mining.AddChild(goMineNode);
        mining.AddChild(mineNode);

        Sequencer<BlackBoard> depositing = new Sequencer<BlackBoard>(blackboard);
        rootNode.AddChild(depositing);
        GoHome goHomeNode = new GoHome(blackboard);
        Deposit depoNode = new Deposit(blackboard);
        depositing.AddChild(goHomeNode);
        depositing.AddChild(depoNode);





        return rootNode;
    }
}
