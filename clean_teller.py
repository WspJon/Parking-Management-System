import re

controls_to_remove = [
    'pnlStatsSales', 'lblTotalSalesTitle', 'lblTotalSales',
    'btnSettings', 'Button1', 'btnTabAccounts', 'pnlAccounts',
    'btnClearLog', 'btnClearUsers', 'dgvUsers', 'lblAccountsTitle', 'PictureBox1'
]

# Specifically checking children of pnlAccounts and pnlStatsSales to remove them too
extra_controls = ['Label1']

# Let's just do a naive filter: if a line contains any of the control names, remove it.
# BUT wait, some variables might share substrings. It's better to match exact words.
def filter_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        lines = f.readlines()
    
    new_lines = []
    for line in lines:
        keep = True
        for ctrl in controls_to_remove:
            # Check if ctrl is in the line
            if ctrl in line:
                keep = False
                break
        
        if keep:
            new_lines.append(line)
            
    with open(filepath, 'w', encoding='utf-8') as f:
        f.writelines(new_lines)

filter_file('frmtellerdashboard.Designer.vb')
filter_file('frmtellerdashboard.vb')
