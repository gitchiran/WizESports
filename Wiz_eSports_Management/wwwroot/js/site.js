var Reports_RegisteredTeamsData = [];


function showMask() {
    $('#preloader').delay(0).fadeIn();
}

function hideMask() {
    $('#preloader').delay(0).fadeOut();
}


// Common Callbacks
$(document).ready(function () {

});

function CallAPI(url, type, params, Section) {
    var val = [];
    $.ajax({
        url: url,
        type: type,
        async: false,
        contentType: "application/json",
        data: JSON.stringify(params),
        success: function (response) {
            if (response != null) {
                var resp = response[0];
                if (response != undefined) {
                    if (Section == 'UpdateTeamPaymentVerificationStatus') {
                        if (response.status == 200) {
                            $('#TournamentTeamRejectionModal').modal('hide');
                            BindRegisteredTournamentTeams();
                            ////alert('Updated Successfully.');
                            DisplaySuccessModal('lblMessage', 'myModal', 'Updated Successfully.');
                        }
                        //////else if (response == "201") {

                        //////}
                    }

                    if (Section == 'UpdateAdminSettings') {
                        if (response.status == 200) {
                            BindData('AdminSettings');
                            DisplaySuccessModal('lblMessage', 'myModal', 'Updated Successfully.');
                        }
                    }

                    if (Section == 'UpdateTournamentMatchDetails') {
                        if (response.status == 200) {
                            ////alert('Updated Successfully.');
                            DisplaySuccessModal('lblMessage', 'myModal', 'Updated Successfully.');
                            ClearControls('Tournament_Match');
                        }
                        if (response.status == 202) {
                            ////alert('Duplicate entry.');
                            DisplayErrorModal('lblMessage', 'myModal', 'Duplicate entry.');
                        }
                    }

                    if (Section == 'TournamentEndDateDetails') {
                        if (response.status == 200) {
                            DisplaySuccessModal('lblMessage', 'myModal', 'End Date updated Successfully.');
                            var Id = getUrlVars()["tournamentId"];
                            PageRedirectClick("TournamentPage", Id);
                        }
                    }

                    if (Section == 'UpdateTournamentDrawStatus') {
                        if (response.status == 200) {
                            ////alert('Updated Successfully.');
                            DisplaySuccessModal('lblMessage', 'myModal', 'Updated Successfully.');
                            ClearControls('TournamentDraw');
                        }
                    }

                    if (Section == 'ResetPasswordLink') {
                        if (response.status == 200) {
                            DisplaySuccessModal('lblMessage', 'myModal', 'Reset Password email sent. Kindly check your email.');
                        }
                        else if (response.status == 201) {
                            DisplayErrorModal('lblMessage', 'myModal', 'An error has occured. Please try again.');
                        }
                        HideLoader();
                    }

                    if (Section == 'ResetPasswordSubmit') {
                        if (response.status == 200) {
                            DisplaySuccessModal('lblMessage', 'myModal', 'Password reset successfully.');
                            ClearControls('ResetPasswordSubmit');
                            window.location.href = "/Home/Login";
                        }
                        else if (response.status == 201) {
                            DisplayErrorModal('lblMessage', 'myModal', 'An error has occured. Please try again.');
                        }
                    }

                    if (Section == 'UpdateTournamentMatchScoreStatus') {
                        if (response.status == 200) {
                            ////alert('Updated Successfully.');
                            DisplayErrorModal('lblMessage', 'myModal', 'Deleted Successfully.');
                            ClearControls('Tournament_Match');
                        }
                    }

                    if (Section == 'TournamentDelete') {
                        if (response.status == 200) {
                            DisplaySuccessModal('lblMessage', 'myModal', 'Tournament Status updated successfully.');
                        }

                        if (response.status == 201) {
                            DisplayErrorModal('lblMessage', 'myModal', 'An error has occured. Please try after some time.');
                        }
                    }

                    if (Section == 'TournamentTeamDetails') {
                        Reports_RegisteredTeamsData = response;
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'TournamentTeamDrawDetails') {
                        BindDropdownData(response, 'TournamentData_Team');
                    }

                    if (Section == 'TournamentTeamMatchDetails') {
                        ////ConvertJsontoDataTable(response, Section);
                        BindDropdownData(response, 'TournamentTeamData_Match');
                    }

                    if (Section == 'TournamentTeamMatchDetailsTable') {
                        ConvertJsontoDataTable(response, Section);
                        ////BindDropdownData(response, 'TournamentTeamData_Match');
                    }

                    if (Section == 'TournamentDrawDetails') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'TournamentTeamPastRegisteredTournament') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'TournamentTeamUpcomingRegisteredTournament') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'TournamentTeamUpcomingUnregisteredTournament') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'TeamDrawDetails') {
                        ConvertJsontoDataTable(response, Section);
                    }
                    if (Section == 'TeamPastRegisteredTournamentDetails') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'UpcomingTournaments_Index') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'LastDrawDetails_Index') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'LastMatchDetails_Index') {
                        ConvertJsontoDataTable(response, Section);
                    }

                    if (Section == 'TeamClanRankDetails') {
                        val = response;
                    }

                    if (Section == 'AdminSettings') {
                        val = response;
                    }

                    if (Section == 'TournamentEndDate') {
                        val = response;
                    }
                }
            }
        },
        error: function (data) {
            ////alert(data);
            DisplayErrorModal('lblMessage', 'myModal', data);
        },
        failure: function (response) {
            a = b;
        }
    });
    return val;
}

function CallAPIGET(url, type, params, Section) {
    $.ajax({
        url: url,
        type: type,
        async: false,
        headers: { 'Access-Control-Allow-Origin': '*' },
        crossDomain: true,
        contentType: "application/json",
        data: params,
        success: function (response) {
            var resp = response[0];
            if (response != null) {
                if (Section == 'UserAuthentication') {
                    if (resp.Result == "101") {
                        var UserSessionData = [];
                        var UserID = resp.UserID;
                        var UserName = resp.UserName;
                        var EmailId = resp.EmailId;
                        var MobileNumber = resp.MobileNumber;

                        // ---Edited by Baskaran---
                        var IsAdmin = resp.IsAdmin;
                        // ---End---

                        UserSessionData.push({
                            UserID: UserID,
                            UserName: UserName,
                            EmailId: EmailId,
                            MobileNumber: MobileNumber,

                            // ---Edited by Baskaran---
                            IsAdmin: IsAdmin
                            // ---End---

                        });

                        ClearControls('UserAuthentication');
                        sessionStorage.setItem('UserSession', JSON.stringify(UserSessionData));
                        sessionStorage.setItem('CategoryID', 0);
                        //$(location).attr('href', 'AllProducts.html');
                        // ---Edited by Baskaran---
                        if (IsAdmin == 'True') {
                            window.location.href = ApplicationUrl + "Admin/AdminActivity";
                        }
                        else {
                            window.location.href = ApplicationUrl + "A2mate/AllProducts";
                        }
                        // ---End---
                    }
                    else if (resp.Result == "102") {
                        DisplayErrorModal('lblMessage', 'LoginModal', 'Invalid Email ID/Password.');
                    }
                    else if (resp.Result == "103") {
                        DisplayErrorModal('lblMessage', 'LoginModal', 'Email not yet validated. Kindly login to email and validate.');
                    }
                }
            }
        },
        error: function (data) {
            OnAPIError(data)
        }
    });
}

function ConvertJsontoDataTable(data, SubMenuSection) {
    var tr;
    var ctrl = '';
    for (var i = 0; i < data.length; i++) {
        if (SubMenuSection == 'TournamentTeamDetails') {
            var Status = "";
            tr = $("<tr id=tblTTD_tr_" + i + " />");
            tr.append("<td id=tblTTD_td_ID_" + i + " style='padding:10px;display:none'><label style='text-decoration:underline;cursor: pointer;' id=tblTTD_lbl_ID_" + i + ">" + data[i].id + "</label></td>");
            tr.append("<td id=tblTTD_td_TournamentId_" + i + " style='padding:10px;display:none'><label id=tblTTD_lbl_TournamentId_" + i + ">" + data[i].tournamentId + "</label></td>");
            tr.append("<td id=tblTTD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTTD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>");
            tr.append("<td id=tblTTD_td_TeamName_" + i + " style='padding:10px;'><label id=tblTTD_lbl_TeamName_" + i + ">" + data[i].teamName + "</label></td>");
            tr.append("<td id=tblTTD_td_EnrollmentDate_" + i + " style='padding:10px;'><label id=tblTTD_lbl_EnrollmentDate_" + i + ">" + data[i].enrollmentDateString + "</label></td>");
            tr.append("<td id=tblTTD_td_PaymentType_" + i + " style='padding:10px;'><label id=tblTTD_lbl_PaymentType_" + i + ">" + data[i].paymentTypeString + "</label></td>");
            tr.append("<td id=tblTTD_td_IsPaymentMade_" + i + " style='padding:10px;'><label id=tblTTD_lbl_IsPaymentMade_" + i + ">" + data[i].isPaymentMadeString + "</label></td>");
            //////tr.append("<td id=tblTTD_td_PaymentProof_" + i + " style='padding:10px;'><a href='javascript:;' onclick=DownloadDocuments('" + data[i].paymentProof +"')><label id=tblTTD_lbl_PaymentProof_" + i + ">Download</label></a></td>");
            tr.append("<td id=tblTTD_td_PaymentProof_" + i + " style='padding:10px;display: grid;'><a href='" + data[i].paymentProof + "'>Preview</a><a href='javascript:;' onclick=DownloadDocuments('" + data[i].paymentProof + "')>Download</a><div class='box'><iframe src='" + data[i].paymentProof + "' width = '800px' height = '500px'></iframe></div></td>");

            if (data[i].isPaymentVerifiedByAdmin == 'N') {
                tr.append("<td id=tblTTD_td_Action_" + i + " style='padding:10px;' class='jsgrid-cell jsgrid-control-field jsgrid-align-left'><input class='Update' onclick=UpdateStatus('TeamPaymentConfirmation','Approve'," + data[i].id + ") type='button' title='Approve'><input class='Cancel m-l-5' onclick=UpdateStatus('TeamPaymentConfirmation','RejectRequest'," + data[i].id + ") type='button' title='Reject'></td>");
            }
            else if (data[i].isPaymentVerifiedByAdmin == 'V') {
                tr.append("<td id=tblTTD_td_Action_" + i + " style='padding:10px;'><label>Verified</label></td>");
            }
            else if (data[i].isPaymentVerifiedByAdmin == 'R') {
                tr.append("<td id=tblTTD_td_Action_" + i + " style='padding:10px;'><label>Rejected</label></td>");
            }

            $('#tblTTD_tbody').append(tr);
            ctrl = 'tblTTD';
        }

        if (SubMenuSection == 'TournamentDrawDetails') {
            tr = $("<tr id=tblTD_tr_" + i + " />");
            tr.append("<td id=tblTD_td_ID_" + i + " style='padding:10px;display:none'><label style='text-decoration:underline;cursor: pointer;' id=tblTD_lbl_ID_" + i + ">" + data[i].id + "</label></td>");
            tr.append("<td id=tblTD_td_TournamentId_" + i + " style='padding:10px;display:none'><label id=tblTD_lbl_TournamentId_" + i + ">" + data[i].tournamentId + "</label></td>");
            tr.append("<td id=tblTD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>");
            tr.append("<td id=tblTD_td_GroupId_" + i + " style='padding:10px;display:none'><label id=tblTD_lbl_GroupId_" + i + ">" + data[i].groupId + "</label></td>");
            tr.append("<td id=tblTD_td_GroupName_" + i + " style='padding:10px;'><label id=tblTD_lbl_GroupName_" + i + ">" + data[i].groupName + "</label></td>");
            tr.append("<td id=tblTD_td_TeamName_" + i + " style='padding:10px;'><label id=tblTD_lbl_TeamName_" + i + ">" + data[i].teamName + "</label></td>");
            tr.append("<td id=tblTD_td_TeamMatchDate_" + i + " style='padding:10px;'><label id=tblTD_lbl_TeamMatchDate_" + i + ">" + data[i].drawDate + "</label></td>");
            tr.append("<td id=tblTTD_td_Action_" + i + " style='padding:10px;' class='jsgrid-cell jsgrid-control-field jsgrid-align-left'><input class='Cancel m-l-5' onclick=UpdateStatus('TournamentDraw','RejectRequest'," + data[i].id + ") type='button' title='Remove'></td>");
            $('#tblTD_tbody').append(tr);
        }

        if (SubMenuSection == 'TournamentTeamMatchDetailsTable') {
            tr = $("<tr id=tblTMD_tr_" + i + " />");
            tr.append("<td id=tblTMD_td_ID_" + i + " style='padding:10px;display:none'><label style='text-decoration:underline;cursor: pointer;' id=tblTMD_lbl_ID_" + i + ">" + data[i].id + "</label></td>");
            tr.append("<td id=tblTMD_td_TournamentId_" + i + " style='padding:10px;display:none'><label id=tblTMD_lbl_TournamentId_" + i + ">" + data[i].tournamentId + "</label></td>");
            tr.append("<td id=tblTMD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTMD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>");
            tr.append("<td id=tblTMD_td_TeamName_" + i + " style='padding:10px;'><label id=tblTMD_lbl_TeamName_" + i + ">" + data[i].teamName + "</label></td>");
            tr.append("<td id=tblTMD_td_ScoreType_" + i + " style='padding:10px;'><label id=tblTMD_lbl_ScoreType_" + i + ">" + data[i].scoreType + "</label></td>");
            tr.append("<td id=tblTMD_td_Score_" + i + " style='padding:10px;'><label id=tblTMD_lbl_Score_" + i + ">" + data[i].score + "</label><input type='text' class='form-control dis-none' id=txtTMD_lbl_Score_" + i + " /></td>");
            tr.append("<td id=tblTMD_td_Action_" + i + " style='padding:10px;' class='jsgrid-cell jsgrid-control-field jsgrid-align-left'><input id='btnEdit_" + i + "' class='Edit' onclick=EditUpdateData('TournamentTeamMatchDetailsTable','Edit'," + i + ") type='button' title='Edit'><input id='btnUpdate_" + i + "' class='Update dis-none' onclick=EditUpdateData('TournamentTeamMatchDetailsTable','Update'," + i + ") type='button' title='Update'><input id='btnCancel_" + i + "' class='Cancel dis-none m-l-5' onclick=EditUpdateData('TournamentTeamMatchDetailsTable','Cancel'," + i + ") type='button' title='Cancel'><input id='btnDelete_" + i + "' class='Delete m-l-5' onclick=UpdateStatus('TournamentMatch','RejectRequest'," + data[i].id + ") type='button' title='Remove'></td>");
            $('#tblTMD_tbody').append(tr);
        }

        if (SubMenuSection == 'TournamentTeamPastRegisteredTournament') {
            debugger;
            var divOuter = "";
            divOuter = divOuter + "<div class='row'>";

            for (var i = 0; i < data.length; i++) {
                var Id = data[i].id;
                var dateString = data[i].sceduledDate;
                ////////if (data[i].sceduledDate.HasValue) {
                ////////    dateString = data[i].sceduledDate.Value.ToString("MMMM dd, yyyy");
                ////////}
                var TournamentName = data[i].tournamentName;
                var TournamentDescription = data[i].tournamentDescription;
                var EntryFee = data[i].entryFee;

                if (TournamentDescription.length > 70) {
                    var TournamentDesc = '';
                    var maxLength = 67;
                    TournamentDesc = TournamentDescription.substr(0, maxLength);

                    divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-container cursor-pointer' onclick=BindData('TeamPastRegisteredTournamentDetails','" + Id + "') data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content tilePaddingUser'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><a onclick=FillUpcomingTournamentDetails(" + i + ") title='Click to view complete details.'><p id=lblTournamentDescription_" + i + " style='word-break: break-all;margin-bottom:0px;' value='" + TournamentDescription + "' >" + TournamentDesc + "<b><span class='lnk m-t-0'>...</span></b></p></a><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
                }
                else {
                    divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-container cursor-pointer' onclick=BindData('TeamPastRegisteredTournamentDetails','" + Id + "') data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content tilePaddingUser'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><p style='word-break: break-all;'>" + TournamentDescription + "</p><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
                }

                ////////divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-container cursor-pointer' onclick=BindData('TeamPastRegisteredTournamentDetails','" + Id + "') data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><p style='word-break: break-all;'>" + TournamentDescription + "</p><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
            }

            divOuter = divOuter + "</div>";

            $('#divTeamPastTournaments').append(divOuter);
        }

        if (SubMenuSection == 'TournamentTeamUpcomingRegisteredTournament') {
            debugger;
            var divOuter = "";
            divOuter = divOuter + "<div class='row'>";

            for (var i = 0; i < data.length; i++) {
                var Id = data[i].id;
                var dateString = data[i].sceduledDate;
                var TournamentName = data[i].tournamentName;
                var TournamentDescription = data[i].tournamentDescription;
                var EntryFee = data[i].entryFee;

                if (TournamentDescription.length > 70) {
                    var TournamentDesc = '';
                    var maxLength = 67;
                    TournamentDesc = TournamentDescription.substr(0, maxLength);

                    divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-container cursor-pointer' onclick=BindData('TeamDrawDetails','" + Id + "') data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content tilePaddingUser'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><a onclick=FillUpcomingTournamentDetails(" + i + ") title='Click to view complete details.'><p id=lblTournamentDescription_" + i + " style='word-break: break-all;margin-bottom:0px;' value='" + TournamentDescription + "' >" + TournamentDesc + "<b><span class='lnk m-t-0'>...</span></b></p></a><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
                }
                else {
                    divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-container cursor-pointer' onclick=BindData('TeamDrawDetails','" + Id + "') data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content tilePaddingUser'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><p style='word-break: break-all;'>" + TournamentDescription + "</p><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
                }
            }

            divOuter = divOuter + "</div>";

            $('#divTeamUpcomingTournaments').append(divOuter);
        }

        if (SubMenuSection == 'TournamentTeamUpcomingUnregisteredTournament') {
            var divOuter = "";
            divOuter = divOuter + "<div class='row'>";

            for (var i = 0; i < data.length; i++) {
                var Id = data[i].id;
                var dateString = data[i].sceduledDate;
                var TournamentName = data[i].tournamentName;
                var TournamentDescription = data[i].tournamentDescription;
                var EntryFee = data[i].entryFee;

                if (TournamentDescription.length > 70) {
                    var TournamentDesc = '';
                    var maxLength = 67;
                    TournamentDesc = TournamentDescription.substr(0, maxLength);

                    divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-containerupcoming cursor-pointer' onclick=PageRedirectClick('TournamentPage'," + Id + ") data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content tilePaddingUser'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><a onclick=FillUpcomingTournamentDetails(" + i + ") title='Click to view complete details.'><p id=lblTournamentDescription_" + i + " style='word-break: break-all;margin-bottom:0px;' value='" + TournamentDescription + "' >" + TournamentDesc + "<b><span class='lnk m-t-0'>...</span></b></p></a><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
                }
                else {
                    divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-containerupcoming cursor-pointer' onclick=PageRedirectClick('TournamentPage'," + Id + ") data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content tilePaddingUser'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><p style='word-break: break-all;'>" + TournamentDescription + "</p><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
                }

                ////////divOuter = divOuter + "<div class='col-lg-6'><a class='tournament-containerupcoming cursor-pointer' onclick=PageRedirectClick('TournamentPage'," + Id + ") data-tournamentid='" + Id + "'><div class='latest-match-box mb-30'><div class='latest-match-thumb'><img src='/theme/img/images/latest_match_thumb01.jpg' alt=''></div><div class='tournament-schedule-content'><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><p style='word-break: break-all;'>" + TournamentDescription + "</p><div class='tournament-schedule-meta tournament-fee'><h5>Entry Fee: <span>" + EntryFee + "</span></h5></div></div></div></a></div>";
            }

            divOuter = divOuter + "</div>";

            $('#divTeamUnregisteredTournaments').append(divOuter);
        }

        if (SubMenuSection == 'UpcomingTournaments_Index') {
            var divOuter = "";
            divOuter = divOuter + "<div class='carousel-inner row w-100 mx-auto col-lg-12 nopadding' role='listbox'>";

            for (var i = 0; i < data.length; i++) {
                var ActiveClass = "";
                var Id = data[i].id;
                var dateString = data[i].sceduledDate;
                var TournamentName = data[i].tournamentName;
                var TournamentDescription = data[i].tournamentDescription;
                var EntryFee = data[i].entryFee;

                if (i == 0) {
                    ActiveClass = " active";
                }

                if (data.length == 1) {
                    divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-4 " + ActiveClass + "'><div class='latest-games-items mb-30'><div class='latest-games-thumb'><img src='/theme/img/product/tournament" + (i + 1) + ".png' alt=''></div><div class='latest-games-content'><h4>" + TournamentName + "</h4><p>Prize : <span>" + EntryFee + "</span></p><p>Event Date : <span class='clryellow'>" + dateString + "</span></p></div></div></div>";

                    divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-4'><div class='latest-games-items mb-30'><div class='latest-games-thumb'><img src='/theme/img/product/NoTournamentImage.png' alt=''></div><div class='latest-games-content'><h4>Pending for Verification</h4><p>Prize : <span>NA</span></p><p>Event Date : <span class='clryellow'>NA</span></p></div></div></div>";

                    divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-4'><div class='latest-games-items mb-30'><div class='latest-games-thumb'><img src='/theme/img/product/NoTournamentImage.png' alt=''></div><div class='latest-games-content'><h4>Pending for Verification</h4><p>Prize : <span>NA</span></p><p>Event Date : <span class='clryellow'>NA</span></p></div></div></div>";
                }
                else if (data.length == 2) {
                    //divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-6 " + ActiveClass + "'><div><div><img src='/theme/img/product/tournament3.png' alt=''></div><div><h3>" + TournamentName + "</h3><h4><span> " + dateString + "</span></h4><p>" + TournamentDescription + "</p><div><h5>Prize: <span>" + EntryFee + "</span></h5></div></div></div></div>";
                    divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-4 " + ActiveClass + "'><div class='latest-games-items mb-30'><div class='latest-games-thumb'><img src='/theme/img/product/tournament" + (i + 1) + ".png' alt=''></div><div class='latest-games-content'><h4>" + TournamentName + "</h4><p>Prize : <span>" + EntryFee + "</span></p><p>Event Date : <span class='clryellow'>" + dateString + "</span></p></div></div></div>";

                    if (i == (data.length - 1)) {
                        divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-4'><div class='latest-games-items mb-30'><div class='latest-games-thumb'><img src='/theme/img/product/NoTournamentImage.png' alt=''></div><div class='latest-games-content'><h4>Pending for Verification</h4><p>Prize : <span>NA</span></p><p>Event Date : <span class='clryellow'>NA</span></p></div></div></div>";
                    }
                }
                else {
                    divOuter = divOuter + "<div class='carousel-item col-12 col-sm-6 col-md-4 col-lg-4 " + ActiveClass + "'><div class='latest-games-items mb-30'><div class='latest-games-thumb'><img src='/theme/img/product/tournament" + (i + 1) + ".png' alt=''></div><div class='latest-games-content'><h4>" + TournamentName + "</h4><p>Prize : <span>" + EntryFee + "</span></p><p>Event Date : <span class='clryellow'>" + dateString + "</span></p></div></div></div>";
                }
            }

            divOuter = divOuter + "</div><a class='carousel-control-prev' href='#carousel-example' role='button' data-slide='prev'><span class='fa fa-angle-left navbutton' aria-hidden='true'></span><span class='sr-only'>Previous</span></a><a class='carousel-control-next' href='#carousel-example' role='button' data-slide='next'><span class='fa fa-angle-right navbutton' aria-hidden='true'></span><span class='sr-only'>Next</span></a>";

            $('#carousel-example').append(divOuter);

            sessionStorage.setItem("SliderLength", data.length);
        }

        if (SubMenuSection == 'TeamDrawDetails') {
            $('#divModalBody').text('');
            $('#lblModalTitle').text('Draw Details');
            var GroupName = '';
            var MatchDate = '';
            var MatchTime = '';
            var Data = "";

            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    GroupName = data[i].groupName;
                    MatchDate = data[i].drawDate.split(' ')[0];
                    MatchTime = data[i].drawDate.split(' ')[1];
                    Data = Data + "<div id='divTeamTournamentDraw' class='p-lg-2'><div class='row nopadding'><span>" + GroupName + "</span></div><div class='row m-t-10 borderblack'><div class='col-lg-6 p-3 text-left'><span>Date: </span><span>" + MatchDate + "</span></div><div class='col-lg-6 p-3 text-right'><span>Time: </span><span>" + MatchTime + "</span></div></div><div class='row m-t-10 nopadding'>";
                }
                var TeamName = data[i].teamName;

                Data = Data + "<div class='col-lg-4'><span>" + TeamName + "</span></div>";
            }
            Data = Data + "</div></div>";

            $('#divModalBody').append(Data);
            $('#ModalDisplay').modal('show');
        }

        if (SubMenuSection == 'TeamPastRegisteredTournamentDetails') {
            $('#divModalBody').text('');
            $('#lblModalTitle').text('Rank Details');

            //////var Data = "<table id='tblTRD' class='col-lg-12 jsgrid-table m-t-10'><thead><tr class='TableHeaderOuter jsgrid-header-row'><th class='jsgrid-header-cell' style='display:none'>Team ID</th><th class='jsgrid-header-cell'>Rank</th><th class='jsgrid-header-cell'>Team Name</th><th class='jsgrid-header-cell'>Score</th><th class='jsgrid-header-cell'>Kills</th><th class='jsgrid-header-cell'>Wins</th></tr></thead><tbody id='tblTMD_tbody' class='col-lg-12'>";
            //////for (var i = 0; i < data.length; i++) {
            //////    Data = Data + "<tr><td id=tblTRD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTRD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>";
            //////    Data = Data + "<td id=tblTRD_td_rank_" + i + " style='padding:10px;'><label id=tblTRD_lbl_rank_" + i + ">" + data[i].rank + "</label></td>";
            //////    Data = Data + "<td id=tblTRD_td_TeamName_" + i + " style='padding:10px;'><label id=tblTRD_lbl_TeamName_" + i + ">" + data[i].teamName + "</label></td>";
            //////    Data = Data + "<td id=tblTRD_td_score_" + i + " style='padding:10px;'><label id=tblTRD_lbl_score_" + i + ">" + data[i].score + "</label></td>";
            //////    Data = Data + "<td id=tblTRD_td_kill_" + i + " style='padding:10px;'><label id=tblTRD_lbl_kill_" + i + ">" + data[i].kills + "</label></td>";
            //////    Data = Data + "<td id=tblTRD_td_wins_" + i + " style='padding:10px;'><label id=tblTRD_lbl_wins_" + i + ">" + data[i].wins + "</label></td></tr>";
            //////}

            var Data = "<table id='tblTRD' class='col-lg-12 jsgrid-table m-t-10'><thead><tr class='TableHeaderOuter jsgrid-header-row' style='text-transform: uppercase;font-family: 'Oxanium', cursive;'><th class='jsgrid-header-cell' style='display:none'>Team ID</th><th class='jsgrid-header-cell IndexPageTableHeader' style='width:100px;color:#FFFFFF;'>Rank</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#FFFFFF;width: 60%;'>Team Name</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#E4A101;'>Wins</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#E4A101;'>Kills</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#E4A101;'>Score</th></tr></thead><tbody id='tblTMD_tbody' class='col-lg-12' style='background: #18142d;'>";
            for (var i = 0; i < data.length; i++) {
                Data = Data + "<tr><td id=tblTRD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTRD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>";
                Data = Data + "<td id=tblTRD_td_rank_" + i + " style='padding:10px;'><label id=tblTRD_lbl_rank_" + i + " style='color:#FFFFFF;font-weight: bold;'>" + data[i].rank + "</label></td>";
                Data = Data + "<td id=tblTRD_td_TeamName_" + i + " style='padding:10px;color:#FFFFFF;'><label id=tblTRD_lbl_TeamName_" + i + " style='color:#FFFFFF;font-weight: bold;'>" + data[i].teamName + "</label></td>";
                Data = Data + "<td id=tblTRD_td_wins_" + i + " style='padding:10px;'><label id=tblTRD_lbl_wins_" + i + " style='color:#E4A101;font-weight: bold;'>" + data[i].wins + "</label></td>";
                Data = Data + "<td id=tblTRD_td_kill_" + i + " style='padding:10px;'><label id=tblTRD_lbl_kill_" + i + " style='color:#E4A101;font-weight: bold;'>" + data[i].kills + "</label></td>";
                Data = Data + "<td id=tblTRD_td_score_" + i + " style='padding:10px;'><label id=tblTRD_lbl_score_" + i + " style='color:#E4A101;font-weight: bold;'>" + data[i].score + "</label></td></tr>";
            }

            Data = Data + "</tbody></table>";

            $('#divModalBody').append(Data);
            $('#ModalDisplay').modal('show');
        }

        if (SubMenuSection == 'LastDrawDetails_Index') {
            debugger;
            var uniqueArray = data.reduce(function (a, d) {
                if (a.indexOf(d.groupName) === -1) {
                    a.push(d.groupName);
                }
                return a;
            }, []);

            var MatchDate = '';
            var MatchTime = '';
            var Data = "";
            for (var j = 0; j < uniqueArray.length; j++) {
                var GroupName = uniqueArray[j];

                ////////Data = Data + "<h3 class='btn-black'>Group Name: <h3 class='clryellow p-l-5'>" + GroupName + "</h3></h3>";
                ////////Data = Data + "<table id='tblTRD' class='col-lg-12 jsgrid-table m-t-10'><thead><tr class='TableHeaderOuter jsgrid-header-row'><th class='jsgrid-header-cell' style='display:none'>Team ID</th><th class='jsgrid-header-cell'>Rank</th><th class='jsgrid-header-cell'>Team Name</th><th class='jsgrid-header-cell'>Score</th><th class='jsgrid-header-cell'>Kills</th></tr></thead><tbody id='tblTMD_tbody' class='col-lg-12'>";
                ////////for (var i = 0; i < data.length; i++) {
                ////////    if (i == 0) {
                ////////        $('#TournamentName').text(data[i].tournamentName);
                ////////    }
                ////////    if (data[i].groupName == GroupName) {
                ////////        Data = Data + "<tr><td id=tblTRD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTRD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>";
                ////////        Data = Data + "<td id=tblTRD_td_rank_" + i + " style='padding:10px;'><label id=tblTRD_lbl_rank_" + i + ">" + data[i].rank + "</label></td>";
                ////////        Data = Data + "<td id=tblTRD_td_TeamName_" + i + " style='padding:10px;'><label id=tblTRD_lbl_TeamName_" + i + ">" + data[i].teamName + "</label></td>";
                ////////        Data = Data + "<td id=tblTRD_td_score_" + i + " style='padding:10px;'><label id=tblTRD_lbl_score_" + i + ">" + data[i].score + "</label></td>";
                ////////        Data = Data + "<td id=tblTRD_td_kill_" + i + " style='padding:10px;'><label id=tblTRD_lbl_kill_" + i + ">" + data[i].kills + "</label></td></tr>";
                ////////    }
                ////////}

                ////////Data = Data + "</tbody></table>";

                for (var i = 0; i < data.length; i++) {
                    if (data[i].groupName == GroupName) {
                        MatchDate = data[i].drawDate.split(' ')[0];
                        MatchTime = data[i].drawDate.split(' ')[1];
                    }
                }

                for (var i = 0; i < data.length; i++) {
                    if (i == 0) {
                        $('#TournamentName').text(data[i].tournamentName);
                        ////GroupName = data[i].groupName;                        
                        Data = Data + "<div id='divTeamTournamentDraw" + j + "' class='p-lg-2 col-lg-12'><div class='row nopadding'><h3 class='btn-black'>Group Name: <h3 class='clryellow p-l-5'> " + GroupName + "</h3></h3></div><div class='row m-t-10 borderblack'><div class='col-lg-6 p-3 text-left btn-black'><span><b>Date: </b></span><span>" + MatchDate + "</span></div><div class='col-lg-6 p-3 text-right btn-black'><span><b>Time: </b></span><span>" + MatchTime + "</span></div></div><div class='row m-t-10 nopadding'>";
                    }
                    var TeamName = data[i].teamName;

                    if (data[i].groupName == GroupName) {
                        Data = Data + "<div class='col-lg-4 btn-black'><span>" + TeamName + "</span></div>";
                    }
                }
                Data = Data + "</div></div>";
            }
            $('#divDrawDetails').append(Data);
        }

        if (SubMenuSection == 'LastMatchDetails_Index') {
            var Data = "<table id='tblTRD' class='col-lg-12 jsgrid-table m-t-10'><thead><tr class='TableHeaderOuter jsgrid-header-row' style='text-transform: uppercase;font-family: 'Oxanium', cursive;'><th class='jsgrid-header-cell' style='display:none'>Team ID</th><th class='jsgrid-header-cell IndexPageTableHeader' style='width:100px;color:#FFFFFF;'>Rank</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#FFFFFF;width: 60%;'>Team Name</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#E4A101;'>Wins</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#E4A101;'>Kills</th><th class='jsgrid-header-cell IndexPageTableHeader' style='color:#E4A101;'>Score</th></tr></thead><tbody id='tblTMD_tbody' class='col-lg-12' style='background: #18142d;'>";
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    $('#spnRankings').text(data[i].tournamentName);
                }
                Data = Data + "<tr><td id=tblTRD_td_TeamId_" + i + " style='padding:10px;display:none'><label id=tblTRD_lbl_TeamId_" + i + ">" + data[i].teamId + "</label></td>";
                Data = Data + "<td id=tblTRD_td_rank_" + i + " style='padding:10px;'><label id=tblTRD_lbl_rank_" + i + " style='color:#FFFFFF;font-weight: bold;'>" + data[i].rank + "</label></td>";
                Data = Data + "<td id=tblTRD_td_TeamName_" + i + " style='padding:10px;color:#FFFFFF;'><label id=tblTRD_lbl_TeamName_" + i + " style='color:#FFFFFF;font-weight: bold;'>" + data[i].teamName + "</label></td>";
                Data = Data + "<td id=tblTRD_td_wins_" + i + " style='padding:10px;'><label id=tblTRD_lbl_wins_" + i + " style='color:#E4A101;font-weight: bold;'>" + data[i].wins + "</label></td>";
                Data = Data + "<td id=tblTRD_td_kill_" + i + " style='padding:10px;'><label id=tblTRD_lbl_kill_" + i + " style='color:#E4A101;font-weight: bold;'>" + data[i].kills + "</label></td>";
                Data = Data + "<td id=tblTRD_td_score_" + i + " style='padding:10px;'><label id=tblTRD_lbl_score_" + i + " style='color:#E4A101;font-weight: bold;'>" + data[i].score + "</label></td></tr>";
            }

            Data = Data + "</tbody></table>";

            $('#divMatchDetails_Index').append(Data);
        }
    }

    ////////if (SubMenuSection != 'DashboardCurrentReading' && SubMenuSection != 'OwnerDetails') {
    //////////////if (SubMenuSection != 'DashboardCurrentReading' && SubMenuSection != 'DashboardCurrentReading_User' && SubMenuSection != 'CurrentBill_User' && SubMenuSection != 'CurrentBill_AllUsers' && SubMenuSection != 'OwnerDetails_Dashboard') {
    //////////////    window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
    //////////////    //////var table = $("#" + ctrl).DataTable({ ordering: false });

    //////////////    if ($.fn.dataTable.isDataTable("#" + ctrl)) {
    //////////////        table = $("#" + ctrl).DataTable({ "pageLength": 25 });
    //////////////    }
    //////////////    else {
    //////////////        table = $("#" + ctrl).DataTable({
    //////////////            ordering: false, "pageLength": 25
    //////////////        });
    //////////////    }


    //////////////    ////////var table = $("#" + ctrl).DataTable().DataTable({ ordering: false });
    //////////////    ////table.fnDraw();
    //////////////    PageCount = data.length;
    //////////////}
}

function FillUpcomingTournamentDetails(rowno) {
    debugger;
    var TournamentDescription = $('#lblTournamentDescription_' + rowno).attr('value');
    //var EventDescription = $('#lblEventDescription_' + rowno)[0].innerText;
    $('#txtUpcomingTournamentDescription').val(TournamentDescription);

    $('#TournamentModal').modal('show');
}

$('#myRegModal').on('hidden.bs.modal', function () {
    PageRedirectClick('IndexPage', 0)
})

function PageRedirectClick(Section, id) {
    debugger;
    if (Section == 'TournamentPage') {
        window.location.href = "/Tournament/Details?tournamentId=" + id;
    }

    if (Section == 'SignInPage') {
        window.location.href = "/Home/Login";
    }

    if (Section == 'IndexPage') {
        window.location.href = "/Home/Index";
    }
}

function EditUpdateData(Section, Action, rowno) {
    if (Section == 'TournamentTeamMatchDetailsTable') {
        if (Action == 'Edit') {
            var Score = $('#tblTMD_lbl_Score_' + rowno).text();
            $('#btnUpdate_' + rowno).removeClass('dis-none');
            $('#btnCancel_' + rowno).removeClass('dis-none');
            $('#btnEdit_' + rowno).addClass('dis-none');
            $('#btnDelete_' + rowno).addClass('dis-none');

            $('#tblTMD_lbl_Score_' + rowno).addClass('dis-none');
            $('#txtTMD_lbl_Score_' + rowno).val(Score).removeClass('dis-none');

        }

        if (Action == 'Update') {
            $('#btnUpdate_' + rowno).addClass('dis-none');
            $('#btnCancel_' + rowno).addClass('dis-none');
            $('#btnEdit_' + rowno).removeClass('dis-none');
            $('#btnDelete_' + rowno).removeClass('dis-none');

            var Id = $('#tblTMD_lbl_ID_' + rowno).text();
            var Score = $('#txtTMD_lbl_Score_' + rowno).val();

            if (Score == '') {
                ////alert('Please enter Points.');
                DisplayErrorModal('lblMessage', 'myModal', 'Please enter Points.');
            } else {
                CallAPI('/Match/UpdateTournamentMatchScore?Id=' + Id + '&Score=' + Score, 'GET', '', 'UpdateTournamentMatchDetails');
            }
        }

        if (Action == 'Cancel') {
            $('#btnUpdate_' + rowno).addClass('dis-none');
            $('#btnCancel_' + rowno).addClass('dis-none');
            $('#btnEdit_' + rowno).removeClass('dis-none');
            $('#btnDelete_' + rowno).removeClass('dis-none');

            $('#tblTMD_lbl_Score_' + rowno).removeClass('dis-none');
            $('#txtTMD_lbl_Score_' + rowno).val('').addClass('dis-none');
        }
    }
}

function UpdateStatus(Section, Action, id) {
    if (Section == 'TeamPaymentConfirmation') {
        if (Action == 'Approve') {
            var teamData = {
                "Id": "", "TournamentId": "", "TournamentName": "", "TeamId": "", "TeamName": "", "EnrollmentDate": "", "EnrollmentDateString": "",
                "PaymentType": "", "PaymentTypeString": "", "IsPaymentMade": "", "IsPaymentMadeString": "", "PaymentProof": "", "IsPaymentVerifiedByAdmin": "", "AdminComments": ""
            };

            teamData.Id = id;
            teamData.IsPaymentVerifiedByAdmin = 'V';
            teamData.AdminComments = '';

            CallAPI('/Team/UpdateTeamPaymentVerificationStatus?Id=' + id + '&IsPaymentVerifiedByAdmin=V', 'GET', '', 'UpdateTeamPaymentVerificationStatus');
        }

        if (Action == 'RejectRequest') {
            $('#lblID').text(id);
            $('#TournamentTeamRejectionModal').modal('show');
        }

        if (Action == 'Rejected') {
            id = $('#lblID').text();
            var teamData = {
                "Id": "", "TournamentId": "", "TournamentName": "", "TeamId": "", "TeamName": "", "EnrollmentDate": "", "EnrollmentDateString": "",
                "PaymentType": "", "PaymentTypeString": "", "IsPaymentMade": "", "IsPaymentMadeString": "", "PaymentProof": "", "IsPaymentVerifiedByAdmin": "", "AdminComments": ""
            };

            var AdminComments = $('#txtRejectionComments').val();
            if (AdminComments == '') {
                ////alert('Please enter Comments.');
                DisplayErrorModal('lblMessage', 'myModal', 'Please enter Comments.');
            }
            else {
                CallAPI('/Team/UpdateTeamPaymentVerificationStatus?Id=' + id + '&IsPaymentVerifiedByAdmin=R&AdminComments=' + AdminComments, 'GET', '', 'UpdateTeamPaymentVerificationStatus');
            }
        }
    }

    if (Section == 'TournamentDraw') {
        if (Action == 'RejectRequest') {
            CallAPI('/Draw/DeleteTournamentDraw?DrawId=' + id, 'GET', '', 'UpdateTournamentDrawStatus');
        }
    }

    if (Section == 'TournamentMatch') {
        if (Action == 'RejectRequest') {
            CallAPI('/Match/DeleteMatchScore?matchId=' + id, 'GET', '', 'UpdateTournamentMatchScoreStatus');
        }
    }

    if (Section == 'TournamentDelete') {
        if (Action == 'DeleteTournament') {
            var tournamentId = getUrlVars()["tournamentId"];
            CallAPI('/Tournament/DeleteTournament?tournamentId=' + tournamentId, 'GET', '', 'TournamentDelete');
        }
    }
}

function UpdateTournamentEndDate() {
    var tournamentId = getUrlVars()["tournamentId"];
    var TournamentEndDate = $('#txtTournamentEndDate').val().replace('T', ' ');

    if (TournamentEndDate != '') {
        CallAPI('/Tournament/UpdateTournamentEndDate?tournamentId=' + tournamentId + '&EndDate=' + TournamentEndDate, 'GET', '', 'TournamentEndDateDetails');
    }
}

function GetTournamentEndDate() {
    debugger;
    var tournamentId = getUrlVars()["tournamentId"];
    var Data = CallAPI('/Tournament/GetTournamentEndDate?tournamentId=' + tournamentId, 'GET', '', 'TournamentEndDate');

    if (Data != "") {
        $('#divEndDateText').show();
        $('#lblEndDate').text(Data);

        $('#btnEndDate').hide();
        $('#divEndDate').hide();
    }
    else {
        $('#divEndDateText').hide();
        $('#lblEndDate').text('');

        $('#btnEndDate').show();
        $('#divEndDate').show();
    }
}


function AddDatatoDB(Section, ctrl) {
    if (Section == 'AssignDraw') {
        var IsValid = true;
        var groupId = $('#cmbGroup').val();
        var teamIds = $('#cmbTeam').val();
        var MatchTime = $('#txtMatchDate').val().replace('T', ' ');
        var tournamentId = getUrlVars()["tournamentId"];

        IsValid = ValidateData('', 'TournamentDraw');

        if (IsValid == true) {
            $("#cmbTeam option:selected").each(function () {
                teamId = $(this).val();

                CallAPI('/Draw/AddTournamentDraw?tournamentId=' + tournamentId + '&groupId=' + groupId + '&teamId=' + teamId + '&MatchDate=' + MatchTime, 'GET', '', 'TournamentDrawDetails');
            });

            DisplaySuccessModal('lblMessage', 'myModal', 'Updated Successfully.');
            ClearControls('TournamentDraw');
        }
    }

    if (Section == 'Tournament_Match') {
        var IsValid = true;
        var groupId = $('#cmbGroup_Match').val();
        var teamId = $('#cmbTeam_Match').val();
        var ScoreType = $('#cmbType_Match').val();
        var Points = $('#txtPoints_Match').val();
        var tournamentId = getUrlVars()["tournamentId"];

        IsValid = ValidateData('', 'Tournament_Match');

        if (IsValid == true) {
            CallAPI('/Match/AddTournamentMatchScore?tournamentId=' + tournamentId + '&ScoreType=' + ScoreType + '&teamId=' + teamId + '&Score=' + Points, 'GET', '', 'UpdateTournamentMatchDetails');
        }
    }

    if (Section == 'AdminSettings') {
        var Link = $('#YouTubeLink').val();

        if (Link != '') {
            CallAPI('/Admin/AddAdminData?YouTubeUrl=' + Link, 'GET', '', 'UpdateAdminSettings');
        }
        else {
            DisplayErrorModal('lblMessage', 'myModal', 'Please enter link.');
        }
    }
}

function DropDownChange(Section) {
    if (Section == 'TeamGroup') {
        var groupId = $('#cmbGroup').val();
        BindRegisteredTournamentDraw(groupId, 0)
        BindRegisteredTournamentTeams();
    }

    if (Section == 'TeamGroup_Match') {
        var groupId = $('#cmbGroup_Match').val();
        BindRegisteredTournamentMatchTeams();
        BindRegisteredTournamentMatchTeamsTable(0);

        window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
    }

    if (Section == 'TournamentTeam_Match') {
        var TeamId = $('#cmbTeam_Match').val();
        BindRegisteredTournamentMatchTeamsTable(TeamId);

        window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
    }
}

function DownloadDocuments(DocPath) {
    var oParam = {
        "DocumentPath": ""
    }

    window.location = "../Team/Download?DocumentPath=" + DocPath;
}

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

    if (Action == 'HideEditForm') {
        //////$('#tournamnt-edit-modal').modal('hide');
        var Id = getUrlVars()["tournamentId"];
        PageRedirectClick("TournamentPage", Id);
    }
}

function HideEnrollmentForm() {
    ClearControls('UserEnrollment');
    $('#enrollment-details-modal').modal('hide');
}

function ClearControls(Section) {
    if (Section == 'UserRegistration') {
        $('#Username').val('');
        $('#Email').val('');
        $('#Password').val('');
        $('#Confirm').val('');
        $('#teamName').val('');
        $('#teamDescription').val('');
        $("#teamLogo").val(null);
        $('#contactName').val('');
        $('#contactNic').val('');
        $('#contactEmail').val('');
        $('#contactPhone').val('');
        $("#chkTandC").prop("checked", false);
    }
    if (Section == 'AdminTournament') {
        $('#txtTournamentName').val('');
        $('#txtTournamentDate').val('');
        $('#txtTournamentContact').val('');
        $('#txtTournamentEntryFee').val('');
        $('#txtTournamentDesc').val('');

        ShowHideTournamentForm('Hide');
    }

    if (Section == 'UserEnrollment') {
        $('#TournamentId').val('');
        $('#PaymentType').val(0);
        $('#IsPaymentMade').val(0);
        $("#PaymentProof").val(null);
    }

    if (Section == 'TournamentDraw') {
        var Default = '--Select--';
        $("#cmbGroup option[value='" + 0 + "']").prop('selected', true);
        $("#divGroup>div>p").prop('title', Default);
        $("#divGroup>div>p>span").html(Default);

        $("#divTeam").html('');
        $('#divTeam').append('<select id="cmbTeam" placeholder="Please select Teams" multiple="multiple" class="search-box">');
        $('#cmbTeam').SumoSelect({ triggerChangeCombined: true, placeholder: "Select Team", csvDispCount: 3, search: true, searchText: 'Enter here.', okCancelInMulti: true });
        $('#cmbGroup').val(0);
        $('#txtMatchDate').val('');
        BindRegisteredTournamentDraw(0, 0);
    }

    if (Section == 'Tournament_Match') {
        var Default = '--Select--';
        $("#cmbGroup_Match option[value='" + 0 + "']").prop('selected', true);
        $("#divGroup_Match>div>p").prop('title', Default);
        $("#divGroup_Match>div>p>span").html(Default);

        $("#cmbTeam_Match option[value='" + 0 + "']").prop('selected', true);
        $("#divTeam_Match>div>p").prop('title', Default);
        $("#divTeam_Match>div>p>span").html(Default);

        $("#cmbType_Match option[value='" + 0 + "']").prop('selected', true);
        $("#divType_Match>div>p").prop('title', Default);
        $("#divType_Match>div>p>span").html(Default);

        $('#txtPoints_Match').val('');

        BindRegisteredTournamentMatchTeamsTable(0);
    }

    if (Section == 'ContactForm') {
        $('#message').val('');
        $('#txtName').val('');
        $('#txtEmail').val('');
    }

    if (Section == 'ResetPasswordSubmit') {
        $('#txtPassword').val('');
        $('#txtConfirmPassword').val('');
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
        var NICNumberRepeatCount = 0;
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

                if (tblNIC == NICNumberRepeatCount) {
                    NICNumberRepeatCount = NICNumberRepeatCount + 1;
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
            else if (NICNumberRepeatCount > 1) {
                ErrorMsg = 'Game Id should be unique.';
                $("#teamPlayerTable").jsGrid("loadData");
            }
            else if (MainPlayerRepeatCount > 4) {
                ErrorMsg = 'Max 4 players can be allocated as Main.';
                $("#teamPlayerTable").jsGrid("loadData");
            }
            else if (ReservePlayerRepeatCount > 2) {
                ErrorMsg = 'Max 2 players can be allocated as Reserve.';
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
                        ErrorMsg = 'Please enter the mentioned fields: Game Id';
                    }
                    else {
                        ErrorMsg = ErrorMsg + ', Game Id';
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
    }

    if (Section == 'UserEnrollment') {
        var IsPaymentMade = $('#IsPaymentMade').val();
        var PaymentType = $('#PaymentType').val();
        //////var PaymentProof = $('#PaymentProof').val();

        if (IsPaymentMade == '0') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please enter the mentioned fields: Payment Made';
            }
            else {
                ErrorMsg = ErrorMsg + ', Payment Made';
            }
        }

        if (PaymentType == '0') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please enter the mentioned fields: Payment Type';
            }
            else {
                ErrorMsg = ErrorMsg + ', Payment Type';
            }
        }

        //////if (PaymentProof == null || PaymentProof == "null" || PaymentProof == '') {
        //////    if (ErrorMsg == '') {
        //////        ErrorMsg = 'Please enter the mentioned fields: Payment Proof';
        //////    }
        //////    else {
        //////        ErrorMsg = ErrorMsg + ', Payment Proof';
        //////    }
        //////}
    }

    if (Section == 'TournamentGroups') {
        var RecordCount = $('.jsgrid-table>tbody tr').length;
        var rows = $('.jsgrid-table>tbody tr');
        var GroupNameRepeatCount = 0;

        var GroupName = args.item.groupName;

        for (i = 0; i < RecordCount; i++) {
            var row = rows[i];

            var tblGroupName = row.cells[0].innerText;

            if (tblGroupName == GroupName) {
                GroupNameRepeatCount = GroupNameRepeatCount + 1;
            }
        }

        if (GroupNameRepeatCount > 1) {
            ErrorMsg = 'Group Name should be unique.';
            $("#TournamentGroupTable").jsGrid("loadData");
        }
        else {
            if (GroupName == '') {
                if (ErrorMsg == '') {
                    ErrorMsg = 'Please enter the mentioned fields: Group Name';
                }
                else {
                    ErrorMsg = ErrorMsg + ', Group Name';
                }
            }
        }
    }

    if (Section == 'TournamentDraw') {
        var groupId = $('#cmbGroup').val();
        var teamId = $('#cmbTeam').val();

        if (groupId == '0') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Group';
            }
            else {
                ErrorMsg = ErrorMsg + ', Group';
            }
        }

        if (teamId.length == 0) {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Team';
            }
            else {
                ErrorMsg = ErrorMsg + ', Team';
            }
        }
    }

    if (Section == 'Tournament_Match') {
        var groupId = $('#cmbGroup_Match').val();
        var teamId = $('#cmbTeam_Match').val();
        var ScoreType = $('#cmbType_Match').val();
        var Points = $('#txtPoints_Match').val();

        if (groupId == '0') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Group';
            }
            else {
                ErrorMsg = ErrorMsg + ', Group';
            }
        }

        if (teamId == '0') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Team';
            }
            else {
                ErrorMsg = ErrorMsg + ', Team';
            }
        }

        if (ScoreType == '0') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Type';
            }
            else {
                ErrorMsg = ErrorMsg + ', Type';
            }
        }

        if (Points == '') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Points';
            }
            else {
                ErrorMsg = ErrorMsg + ', Points';
            }
        }
    }

    if (Section == 'AddTournament') {
        var TournamentName = $('#txtTournamentName').val();
        var TournamentDate = $('#txtTournamentDate').val();
        var TournamentContact = $('#txtTournamentContact').val();
        var TournamentEntryFee = $('#txtTournamentEntryFee').val();
        var TournamentDesc = $('#txtTournamentDesc').val();

        if (TournamentName == '') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Name';
            }
            else {
                ErrorMsg = ErrorMsg + ', Name';
            }
        }

        if (TournamentDate == '') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Date';
            }
            else {
                ErrorMsg = ErrorMsg + ', Date';
            }
        }

        if (TournamentContact == '') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Contact';
            }
            else {
                ErrorMsg = ErrorMsg + ', Contact';
            }
        }

        if (TournamentEntryFee == '') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Entry Fee';
            }
            else {
                ErrorMsg = ErrorMsg + ', Entry Fee';
            }
        }

        if (TournamentDesc == '') {
            if (ErrorMsg == '') {
                ErrorMsg = 'Please select the mentioned fields: Description';
            }
            else {
                ErrorMsg = ErrorMsg + ', Description';
            }
        }

    }

    if (ErrorMsg != '') {
        IsValid = false;
        if (Section == 'UserTeam' || Section == 'TournamentGroups') {
            args.cancel = true;
        }
        ////alert(ErrorMsg);
        DisplayErrorModal('lblMessage', 'myModal', ErrorMsg);
    }

    return IsValid;
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function GetEnrollmentStatus() {
    var tournamentId = getUrlVars()["tournamentId"];
    $.ajax({
        type: 'get',
        url: '/Tournament/GetEnrollmentStatus?TournamentId=' + tournamentId,
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.status == 200) {
                $('#btnEnroll_User').show();
                $('#lblEnroll_User').hide();
            }
            else {
                $('#btnEnroll_User').hide();
                $('#lblEnroll_User').show();
            }
        },
        error: function () {
            //Failed
        }
    });
}

function BindRegisteredTournamentTeams() {
    //////$('#tblTTD_tbody').html('');
    var tournamentId = getUrlVars()["tournamentId"];
    var groupId = $('#cmbGroup').val();
    if (groupId == '0') {
        $("#divTeam").html('');
        $('#divTeam').append('<select id="cmbTeam" placeholder="Please select Teams" multiple="multiple" class="search-box">');
        $('#cmbTeam').SumoSelect({ triggerChangeCombined: true, placeholder: "Select Team", csvDispCount: 3, search: true, searchText: 'Enter here.', okCancelInMulti: true });
    }
    else {
        CallAPI('/Team/GetDrawRegisteredTournamentTeams?tournamentId=' + tournamentId + '&groupId=' + groupId, 'GET', '', 'TournamentTeamDrawDetails');
    }
}

function BindRegisteredTournamentMatchTeams() {
    $('#tblTMD_tbody').html('');
    var tournamentId = getUrlVars()["tournamentId"];
    var groupId = $('#cmbGroup_Match').val();

    if (groupId == '0') {
        $("#divTeam_Match").html('');
        $('#divTeam_Match').append('<select id="cmbTeam_Match" onchange=DropDownChange("TournamentTeam_Match") placeholder="Please select Team" class="search-box"><option selected value="0">--Select--</option>');
        window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
    }
    else {
        //////CallAPI('/Team/GetRegisteredTournamentTeams?tournamentId=' + tournamentId, 'GET', '', 'TournamentTeamMatchDetails');
        CallAPI('/Team/GetMatchRegisteredTournamentTeams?tournamentId=' + tournamentId + '&groupId=' + groupId, 'GET', '', 'TournamentTeamMatchDetails');
    }
}

function BindRegisteredTournamentTeamsTable() {
    $('#tblTTD_tbody').html('');
    var tournamentId = getUrlVars()["tournamentId"];
    CallAPI('/Team/GetRegisteredTournamentTeams?tournamentId=' + tournamentId, 'GET', '', 'TournamentTeamDetails');
}

function BindRegisteredTournamentDraw(groupId, teamId) {
    $('#tblTD_tbody').html('');
    var tournamentId = getUrlVars()["tournamentId"];
    CallAPI('/Draw/GetTournamentDrawDetails?tournamentId=' + tournamentId + '&groupId=' + groupId + '&=teamId=' + teamId, 'GET', '', 'TournamentDrawDetails');
}

function BindRegisteredTournamentMatchTeamsTable(teamId) {
    $('#tblTMD_tbody').html('');
    var tournamentId = getUrlVars()["tournamentId"];
    var teamId = $('#cmbTeam_Match').val();
    CallAPI('/Match/GetTournamentMatchScoreDetails?tournamentId=' + tournamentId + '&=teamId=' + teamId, 'GET', '', 'TournamentTeamMatchDetailsTable');
}

function BindData(Section, id) {
    if (Section == 'TournamentTeamPastRegisteredTournament') {
        $('#divTeamPastTournaments').html('');
        CallAPI('/Tournament/GetTeamPastRegisteredTournaments', 'GET', '', 'TournamentTeamPastRegisteredTournament');
    }

    if (Section == 'TournamentTeamUpcomingRegisteredTournament') {
        $('#divTeamUpcomingTournaments').html('');
        CallAPI('/Tournament/GetTeamUpcomingRegisteredTournaments', 'GET', '', 'TournamentTeamUpcomingRegisteredTournament');
    }

    if (Section == 'TournamentTeamUpcomingUnregisteredTournament') {
        $('#divTeamUnregisteredTournaments').html('');
        CallAPI('/Tournament/GetTeamUpcomingUnregisteredTournaments', 'GET', '', 'TournamentTeamUpcomingUnregisteredTournament');
    }

    if (Section == 'TeamDrawDetails') {
        ////$('#divTeamUnregisteredTournaments').html('');
        CallAPI('/Draw/GetTournamentTeamDrawDetails?tournamentId=' + id, 'GET', '', 'TeamDrawDetails');
    }

    if (Section == 'TeamPastRegisteredTournamentDetails') {
        CallAPI('/Match/GetTournamentMatchScoreRankingDetails?tournamentId=' + id, 'GET', '', 'TeamPastRegisteredTournamentDetails');
    }

    if (Section == 'TeamClanRankDetails') {
        debugger;
        var teamid = 0;
        var Id = getUrlVars()["teamId"];

        if (Id > 0) {
            teamid = Id;
        }
        var Data = CallAPI('/Match/GetTournamentMatchScoreClanRank?section=Team&teamId=' + teamid, 'GET', '', 'TeamClanRankDetails');

        if (Data != null) {
            var Rank = Data[0].rank;
            var Total = Data[0].totalCount;
            var ClanRanking = Rank + "/" + Total;
            $('#spnClanRanking').text(" " + ClanRanking);
        }
    }

    if (Section == 'AdminSettings') {
        var Data = CallAPI('/Admin/GetAdminData', 'GET', '', 'AdminSettings');

        if (Data != null) {
            var link = Data[0].youtubeUrl;
            $('#YouTubeLink').attr("href", link).val(link);
        }
    }

    if (Section == 'UpcomingTournaments_Index') {
        $('#carousel-example').html('');
        CallAPI('/Tournament/GetUpcomingTournamentsJson', 'GET', '', 'UpcomingTournaments_Index');
    }

    if (Section == 'LastDrawDetails_Index') {
        $('#divDrawDetails').html('');
        CallAPI('/Draw/GetTournamentIndexDrawDetails', 'GET', '', 'LastDrawDetails_Index');
    }

    if (Section == 'LastMatchDetails_Index') {
        $('#divMatchDetails_Index').html('');
        CallAPI('/Match/GetTournamentMatchScoreClanRank?section=TournamentbySchedule', 'GET', '', 'LastMatchDetails_Index');
    }
}

function BindDropdownData(data, Section) {
    if (Section == 'TournamentData_Group') {
        $("#divGroup").html('');
        $("#divGroup_Match").html('');
        var GroupDropDown = '';
        GroupDropDown = GroupDropDown + '<select id=cmbGroup onchange=DropDownChange("TeamGroup") class="search-box"><option selected value="0">--Select--</option>';
        if (data != null) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                GroupDropDown = GroupDropDown + '<option value="' + value.id + '">' + value.groupName + '</option>';
            });
            GroupDropDown = GroupDropDown + '</select>';
        }
        $('#divGroup').append(GroupDropDown);

        GroupDropDown = '';
        GroupDropDown = GroupDropDown + '<select id=cmbGroup_Match onchange=DropDownChange("TeamGroup_Match") class="search-box"><option selected value="0">--Select--</option>';
        if (data != null) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                GroupDropDown = GroupDropDown + '<option value="' + value.id + '">' + value.groupName + '</option>';
            });
            GroupDropDown = GroupDropDown + '</select>';
        }
        $('#divGroup_Match').append(GroupDropDown);

        window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
    }

    if (Section == 'TournamentData_Team') {
        $("#divTeam").html('');
        var TeamDropDown = '';
        TeamDropDown = TeamDropDown + '<select id=cmbTeam placeholder="Please select Teams" multiple="multiple" class="search-box">'
        if (data != null) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                TeamDropDown = TeamDropDown + '<option value="' + value.teamId + '">' + value.teamName + '</option>';
            });
            TeamDropDown = TeamDropDown + '</select>';
        }
        $('#divTeam').append(TeamDropDown);
        ////window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.', okCancelInMulti: true, selectAll: true });
        $('#cmbTeam').SumoSelect({ triggerChangeCombined: true, placeholder: "Select Team", csvDispCount: 3, search: true, searchText: 'Enter here.', okCancelInMulti: true });
    }

    if (Section == 'TournamentTeamData_Match') {
        $("#divTeam_Match").html('');
        var TeamDropDown = '';
        TeamDropDown = TeamDropDown + '<select id=cmbTeam_Match onchange=DropDownChange("TournamentTeam_Match") class="search-box"><option selected value="0">--Select--</option>'
        if (data != null) {
            $.each(data, function (index, value) {
                // APPEND OR INSERT DATA TO SELECT ELEMENT.
                TeamDropDown = TeamDropDown + '<option value="' + value.teamId + '">' + value.teamName + '</option>';
            });
            TeamDropDown = TeamDropDown + '</select>';
        }
        $('#divTeam_Match').append(TeamDropDown);
        window.Search = $('.search-box').SumoSelect({ csvDispCount: 3, search: true, searchText: 'Enter here.' });
    }
}

function DisplayErrorModal(lblID, ModalID, Message) {
    $('#' + lblID).text("");
    $('#' + lblID).text(Message);
    $('#' + lblID).removeClass('Duplicate').addClass('Success');
    $('#' + ModalID).modal('show');
}

function DisplaySuccessModal(lblID, ModalID, Message) {
    $('#' + lblID).text("");
    $('#' + lblID).text(Message);
    $('#' + lblID).removeClass('Success').addClass('Duplicate');
    $('#' + ModalID).modal('show');
}

function ShowLoader() {
    $('#cover-spin').show(0);
}

function HideLoader() {
    $('#cover-spin').hide();
}

function ViewData(Section) {
    if (Section == 'PrivacyPolicy') {
        $('#lblModalPDFTitle').text('Privacy Policy');
        $('#iFrameModalPDF').attr('src', '/theme/Documents/PrivacyPolicy.pdf');
    }

    if (Section == 'TermsOfUse') {
        $('#lblModalPDFTitle').text('Terms Of Use');
        $('#iFrameModalPDF').attr('src', '/theme/Documents/TermsOfUse.pdf');
    }

    if (Section == 'AboutUs') {
        $('#lblModalPDFTitle').text('About Us');
        $('#iFrameModalPDF').attr('src', '/theme/Documents/AboutUs.pdf');
    }

    $('#ModalPDFDisplay').modal('show');
}

function PageActive(Section) {
    $('#litournaments').removeClass("show");
    $('#liteams').removeClass("show");
    $('#liusers').removeClass("show");
    $('#liSettings').removeClass("show");
    $('#liHome').removeClass("show");
    $('#liAbout').removeClass("show");
    $('#liContact').removeClass("show");
    $('#liDraw').removeClass("show");
    $('#liHome1').removeClass("show");

    $('#' + Section).addClass("show");
}

function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    //////CSV += ReportTitle + '\r\n\n';

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            if (ReportTitle == "Registered Tournament Teams") {
                if (index == 'tournamentName' || index == 'teamName' || index == 'paymentTypeString' || index == 'isPaymentMadeString') {
                    //Now convert each value to string and comma-seprated
                    row += index.replace("String", "") + ',';
                }

            }
            else {
                //Now convert each value to string and comma-seprated
                row += index + ',';
            }
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            if (ReportTitle == "Registered Tournament Teams") {
                if (index == 'tournamentName' || index == 'teamName' || index == 'paymentTypeString' || index == 'isPaymentMadeString') {
                    if (index == 'tournamentName') {
                        row += '"' + $('#spnTournamentName').text() + '",';
                    }
                    else {
                        row += '"' + arrData[i][index] + '",';
                    }
                }
            }
            else {
                row += '"' + arrData[i][index] + '",';
            }
        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = ReportTitle;
    //this will remove the blank-spaces from the title and replace it with an underscore
    ////fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = fileName + ".csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function DownloadRegisteredTeams() {
    JSONToCSVConvertor(Reports_RegisteredTeamsData, "Registered Tournament Teams", true);
}

function ForgotPassword() {
    var UserName = $('#Username').val();

    if (UserName == '') {
        DisplayErrorModal('lblMessage', 'myModal', 'Please enter User Name.');
    }
    else {
        ShowLoader();
        setTimeout(function () {
            CallAPI('/Home/ResetPasswordLink?UserName=' + UserName, 'GET', '', 'ResetPasswordLink');
        }, 1000);
    }
}

function ResetPasswordSubmit() {
    var Password = $('#txtPassword').val();
    var ConfirmPassword = $('#txtConfirmPassword').val();
    var UserName = getUrlVars()["id"]

    if (Password != ConfirmPassword) {
        DisplayErrorModal('lblMessage', 'myModal', 'Password and Confirm password does not match.');
    }
    else {
        CallAPI('/Home/ResetPassword?UserName=' + UserName + '&Password=' + Password, 'GET', '', 'ResetPasswordSubmit');
    }
}
