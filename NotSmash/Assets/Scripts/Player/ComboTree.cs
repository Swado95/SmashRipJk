using System.Collections;
using System.Collections.Generic;

public class ComboTree {

	private ComboTreeNode root;
	private ComboTreeNode current;

	public ComboTree (List<string> combos) {

		root = new ComboTreeNode();
		CreateComboTree (combos);
	}

	public void CreateComboTree (List<string> combos) {

		for (int i = 0; i < combos.Count; i++) {
			current = root;

			foreach (char button in combos[i]) {
				current.AddChild (new ComboTreeNode (button, current));
				current = current.GetChild (button);
			}
		}

		current = root;
	}

	public bool ButtonIsInCurrentCombo (char button) {

		ComboTreeNode node = current.GetChild (button);

		if (node == null) {
			current = root;
			return false;

		} else {
			current = node;
			return true;
		}
	}
}
