
function showMask() {
    $('#preloader').delay(0).fadeIn();
}

function hideMask() {
    $('#preloader').delay(0).fadeOut();
}


// Common Callbacks
$(document).ready(function () {  

});


function BindAddTournament_Grid() {
    $.ajax({
        url: '../Tournament/AddTournamentPopup',
        type: 'POST',
        async: false,
        contentType: "application/json",
        dataType: 'html',
        //data: JSON.stringify(TransactionData),
        success: function (content) {
            if (content != null) {
                $("#tournament-add").html('');
                $("#tournament-add").html(content);
                ////////window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
            }
        },
        failure: function (response) {
            a = b;
        }
    });
}

function ShowHideTournamentForm(Action) {
    if (Action == 'Show') {
        $('#tournament-section').hide();
        $('#btnAddTournament').hide();
        $('#tournament-add').show();
    }

    if (Action == 'Hide') {
        $('#tournament-section').show();
        $('#btnAddTournament').show();
        $('#tournament-add').hide();
    }
}

function ClearControls(Section) {
    if (Section=='AdminTournament') {
        $('#txtTournamentName').val('');
        $('#txtTournamentDate').val('');
        $('#txtTournamentContact').val('');
        $('#txtTournamentEntryFee').val('');
        $('#txtTournamentDesc').val('');

        ShowHideTournamentForm('Hide');
    }
}

function ValidateNumber(ctrl, DecimalPrecision) {
    var input = $(ctrl);
    var newval = input.val();

    if (!$.isNumeric(newval)) {
        input.val(newval.substr(0, newval.length - 1));
    }

    if (newval.indexOf(".") !== -1) {
        parts = newval.split(".");
        if (parts[1].length > DecimalPrecision) {
            if (DecimalPrecision == 0) {
                input.val(newval.substr(0, newval.length - 2));
            }
            else {
                input.val(newval.substr(0, newval.length - 1));
            }
        }
    }
}

function ValidateData(args, Section) {
    debugger;
    var IsValid = true;
    var ErrorMsg = '';
    if (Section == 'UserTeam') {
        var RecordCount = $('.jsgrid-table>tbody tr').length;
        var rows = $('.jsgrid-table>tbody tr');        
        var UserNameRepeatCount = 0;
        var MainPlayerRepeatCount = 0;
        var ReservePlayerRepeatCount = 0;
        if (RecordCount <= 6) {
            var UserName = args.item.userName;
            var PlayerName = args.item.playerName;
            var NICNumber = args.item.nic;
            var PlayerTag = args.item.playerTag;

            for (i = 0; i < RecordCount; i++) {
                var row = rows[i];

                var tblUserName = row.cells[0].innerText;
                var tblPlayerName = row.cells[1].innerText;
                var tblNIC = row.cells[2].innerText;
                var tblPlayerTag = row.cells[3].innerText;

                if (tblUserName == UserName) {
                    UserNameRepeatCount = UserNameRepeatCount + 1;
                }

                if (tblPlayerTag == 'Main') {
                    MainPlayerRepeatCount = MainPlayerRepeatCount + 1;
                }
                else {
                    ReservePlayerRepeatCount = ReservePlayerRepeatCount + 1;
                }
            }

            if (UserNameRepeatCount > 1) {
                ErrorMsg = 'User Name should be unique.';
                $("#teamPlayerTable").jsGrid("loadData");
            }
            else if (MainPlayerRepeatCount > 4) {
                ErrorMsg = 'Max 4 players can be alloted as Main.';
                $("#teamPlayerTable").jsGrid("loadData");
            }
            else if (ReservePlayerRepeatCount > 2) {
                ErrorMsg = 'Max 2 players can be alloted as Reserve.';
                $("#teamPlayerTable").jsGrid("loadData");
            }
            else {
                if (UserName == '') {
                    if (ErrorMsg == '') {
                        ErrorMsg = 'Please enter the mentioned fields: User Name';
                    }
                    else {
                        ErrorMsg = ErrorMsg + ', User Name';
                    }
                }

                if (PlayerName == '') {
                    if (ErrorMsg == '') {
                        ErrorMsg = 'Please enter the mentioned fields: Player Name';
                    }
                    else {
                        ErrorMsg = ErrorMsg + ', Player Name';
                    }
                }

                if (NICNumber == '') {
                    if (ErrorMsg == '') {
                        ErrorMsg = 'Please enter the mentioned fields: NIC Number';
                    }
                    else {
                        ErrorMsg = ErrorMsg + ', NIC Number';
                    }
                }

                if (PlayerTag == '0') {
                    if (ErrorMsg == '') {
                        ErrorMsg = 'Please enter the mentioned fields: Main/Reserve';
                    }
                    else {
                        ErrorMsg = ErrorMsg + ', Main/Reserve';
                    }
                }
            }            
        }
        else {
            ErrorMsg = 'You cannot have more than 6 players in a Team.';
            $("#teamPlayerTable").jsGrid("loadData");
        }


        if (ErrorMsg != '') {
            IsValid = false;
            args.cancel = true;
            alert(ErrorMsg);
        }
    }

    return IsValid;
}