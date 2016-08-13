using System.Collections.Generic;

#region usingb static

using static LightPlayer.MPlayerControlGroup;

#endregion usingb static

namespace LightPlayer
{
    public class MPlayerControlGroupState
    {
        readonly List<MPlayerControlGroup> _mediaCtrls;

        public MPlayerControlGroupState( List<MPlayerControlGroup> mediaControlsList )
        {
            _mediaCtrls = mediaControlsList;
        }

        public void SetState( int index, MPlayerControlGroupStateEnum state )
        {
            switch ( state )
            {
                case MPlayerControlGroupStateEnum.Stop:

                    _mediaCtrls.ForEach( x =>
                    {
                        x.LoopCheckBox.Enabled = true;
                        x.ClearButton.Enabled = true;
                    } );
                    _mediaCtrls[index].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MPlayerControlGroupStateEnum.StopFromPlaying:
                    _mediaCtrls.ForEach( x =>
                    {
                        x.LoopCheckBox.Enabled = false;
                        x.ClearButton.Enabled = false;
                    } );

                    _mediaCtrls[index].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_STOP;
                    break;

                case MPlayerControlGroupStateEnum.Playing:

                    _mediaCtrls.ForEach( x =>
                    {
                        x.LoopCheckBox.Enabled = false;
                        x.ClearButton.Enabled = false;
                    } );
                    _mediaCtrls[index].FileNameTextBox.ForeColor = COLOR_FILENAME_TEXTBOX_PLAYING;
                    break;
            }
        }
    }
}