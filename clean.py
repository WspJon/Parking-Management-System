import re

def clean_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Replace Imports
    content = re.sub(r'Imports MySql\.Data\.MySqlClient\n?', '', content)
    
    # Find all Try...Catch MySqlException blocks and replace with DataStore.SaveDatabase()
    # Note: we use non-greedy .*? to only match the immediate block.
    # We specifically target blocks that declare a MySqlConnection
    
    pattern1 = r'Try\s*Using\s+conn\s+As\s+MySqlConnection.*?Catch\s+ex\s+As\s+MySqlException.*?End\s+Try'
    content = re.sub(pattern1, 'DataStore.SaveDatabase()', content, flags=re.IGNORECASE | re.DOTALL)

    pattern2 = r'Try\s*Using\s+conn\s+As\s+New\s+MySql\.Data\.MySqlClient\.MySqlConnection.*?Catch\s+ex\s+As\s+Exception.*?End\s+Try'
    content = re.sub(pattern2, 'DataStore.SaveDatabase()', content, flags=re.IGNORECASE | re.DOTALL)
    
    # Remove ParkingData.SyncParkingFromDatabase() since we now save instead
    content = re.sub(r'ParkingData\.SyncParkingFromDatabase\(\)', '', content)
    
    with open(filepath + '.cleaned', 'w', encoding='utf-8') as f:
        f.write(content)

clean_file('DashBoardForm.vb')
