using System.Collections;

public class ComboTreeNode {

	public char button;

	public ComboTreeNode parent;

	public ComboTreeNode normal;
	public ComboTreeNode ranged;
	public ComboTreeNode power;
	public ComboTreeNode jump;

	public ComboTreeNode () : this ('\0', null) { }

	public ComboTreeNode (char button, ComboTreeNode parent) {
		this.button = button;
		this.parent = parent;
	}

	public void AddChild (ComboTreeNode node) {

		if(node.button == 'n' && normal == null){
			normal = node;
		}else if(node.button == 'r' && ranged == null){
			ranged = node;
		}else if(node.button == 'p' && power == null){
			power = node;
		}else if(node.button == 'j' && jump == null){
			jump = node;
		}
	}

	public ComboTreeNode GetChild (char button) {

		if(button == 'n'){
			return normal;
		}else if(button == 'r'){
			return ranged;
		}else if(button == 'p'){
			return power;
		}else if(button == 'j'){
			return jump;
		}

		return null;
	}
}